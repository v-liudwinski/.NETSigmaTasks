using System.Runtime.CompilerServices;
using System.Threading.Channels;
using Homework6_LiudvynskyiV.S.Models;

namespace Homework6_LiudvynskyiV.S.Services;

public class AccountingService
{
    private readonly Accounting _accounting;
    private readonly string _rootPath;
    private readonly string _resultPath;

    private const double PricePerKilowatt = 0.04;

    public AccountingService(Accounting accounting, string rootPath, string resultPath)
    {
        _accounting = accounting;
        _rootPath = rootPath;
        _resultPath = resultPath;
    }

    public void ReadFile()
    {
        var file = File.ReadAllLines(_rootPath);
        var flatsQntAndCounterNum = file.First().Split(", ");
        var inmates = file.Skip(1);

        if (int.TryParse(flatsQntAndCounterNum[0], out var flatsQnt))
            _accounting.FlatsQnt = flatsQnt;
        if (int.TryParse(flatsQntAndCounterNum[1], out var quarterNum))
            _accounting.QuarterNumber = quarterNum;

        var inmatesAttributes = inmates
            .Select(x => x.Split(", "))
            .Select(x =>
            {
                var inmateInfo = new InmateInformation();
                var counterCheck = new CounterCheck[]
                {
                    new CounterCheck(),
                    new CounterCheck(),
                    new CounterCheck()
                };

                if (int.TryParse(x[0], out int flatNum))
                    inmateInfo.FlatNumber = flatNum;
                inmateInfo.Street = x[1];
                inmateInfo.InmateSurname = x[2];

                if (double.TryParse(x[3], out double fCounterIncome))
                    counterCheck[0].CounterIncome = fCounterIncome;

                if (DateTime.TryParse(x[4], out var fDateOfFilming))
                    counterCheck[0].DateOfFilming = fDateOfFilming;

                if (double.TryParse(x[5], out double sCounterIncome))
                    counterCheck[1].CounterIncome = sCounterIncome;

                if (DateTime.TryParse(x[6], out var sDateOfFilming))
                    counterCheck[1].DateOfFilming = sDateOfFilming;

                if (double.TryParse(x[7], out double tCounterIncome))
                    counterCheck[2].CounterIncome = tCounterIncome;

                if (DateTime.TryParse(x[8], out var tDateOfFilming))
                    counterCheck[2].DateOfFilming = tDateOfFilming;

                inmateInfo.CounterChecks = counterCheck;
                return inmateInfo;
            })
            .ToList();

        _accounting.InmatesInformation = inmatesAttributes;
    }

    public Accounting GetAccounting() => _accounting;

    public void WriteResult()
    {
        var result = string.Empty;
        string firstMonth = "", secondMonth = "", thirdMonth = "";

        (firstMonth, secondMonth, thirdMonth) = CorrelateQuarterMonths(firstMonth, secondMonth, thirdMonth);

        result += $"{firstMonth}:\n";
        foreach (var inmateInformation in _accounting.InmatesInformation)
        {
            var flat = inmateInformation.FlatNumber;
            var surname = inmateInformation.InmateSurname;
            var date = $"{inmateInformation.CounterChecks.First().DateOfFilming:dd.MM.yy}";
            var check = $"{GetCheck(inmateInformation, 1):C}";
            var daysFromLastCounterCheck = (DateTime.Now - inmateInformation.CounterChecks.First().DateOfFilming).Days;
            result += $"{flat} {surname} {date} {check} {daysFromLastCounterCheck}\n";
        }

        result += $"{secondMonth}:\n";
        foreach (var inmateInformation in _accounting.InmatesInformation)
        {
            var flat = inmateInformation.FlatNumber;
            var surname = inmateInformation.InmateSurname;
            var date = $"{inmateInformation.CounterChecks.Skip(1).First().DateOfFilming:dd.MM.yy}";
            var check = $"{GetCheck(inmateInformation, 2):C}";
            var daysFromLastCounterCheck =
                (DateTime.Now - inmateInformation.CounterChecks.Skip(1).First().DateOfFilming).Days;
            result += $"{flat} {surname} {date} {check} {daysFromLastCounterCheck}\n";
        }

        result += $"{thirdMonth}:\n";
        foreach (var inmateInformation in _accounting.InmatesInformation)
        {
            var flat = inmateInformation.FlatNumber;
            var surname = inmateInformation.InmateSurname;
            var date = $"{inmateInformation.CounterChecks.Last().DateOfFilming:dd.MM.yy}";
            var check = $"{GetCheck(inmateInformation, 3):C}";
            var daysFromLastCounterCheck = (DateTime.Now - inmateInformation.CounterChecks.Last().DateOfFilming).Days;
            result += $"{flat} {surname} {date} {check} {daysFromLastCounterCheck}\n";
        }

        result += $"The inmate with highest debt: {GetInmateWithMaxDebt()}\n";
        result += $"The flat without electricity: {GetFlatWithoutElectricity()}";
        
        File.WriteAllText(_resultPath, result);
    }

    private double GetCheck(InmateInformation inmateInformation, int quarterStage)
    {
        double check = 0;
        switch (quarterStage)
        {
            case 1:
                check = 0;
                break;
            case 2:
                var outcome = inmateInformation.CounterChecks.ToArray()[1].CounterIncome;
                var income = inmateInformation.CounterChecks.First().CounterIncome;
                check = (outcome - income) * PricePerKilowatt;
                break;
            case 3:
                outcome = inmateInformation.CounterChecks.Last().CounterIncome;
                income = inmateInformation.CounterChecks.First().CounterIncome;
                check = (outcome - income) * PricePerKilowatt;
                break;
            default:
                break;
        }
        return check;
    }

    private int GetFlatWithoutElectricity()
    {
        var checks = CountChecksForElectricity();
        var flats = _accounting.InmatesInformation
            .Select((x, i) => new
            {
                Flat = x.FlatNumber,
                Debt = checks[i]
            })
            .ToArray();
        var flat = flats.Single(x => x.Debt == 0).Flat;
        return flat;
    }
    
    private string GetInmateWithMaxDebt()
    {
        var checks = CountChecksForElectricity();
        var inmates = _accounting.InmatesInformation
            .Select((x, i) => new
            {
                Debt = checks[i],
                Surname = x.InmateSurname
            })
            .ToArray();
        var maxDebt = inmates.Max(x => x.Debt);
        var inmate = inmates.Single(x => x.Debt == maxDebt).Surname;
        return inmate;
    }
    
    private double[] CountChecksForElectricity()
    {
        var checks = new List<double>();
        foreach (var inmateInformation in _accounting.InmatesInformation)
        {
            var income = inmateInformation.CounterChecks.First().CounterIncome;
            var outcome = inmateInformation.CounterChecks.Last().CounterIncome;
            var kilowatts = outcome - income;
            var check = kilowatts * PricePerKilowatt;
            
            checks.Add(check);
        }
        
        return checks.ToArray();
    }

    private (string, string, string) CorrelateQuarterMonths(string firstMonth, string secondMonth, string thirdMonth)
    {
        switch (_accounting.QuarterNumber)
        {
            case 1:
                firstMonth = "January";
                secondMonth = "February";
                thirdMonth = "March";
                break;
            case 2:
                firstMonth = "April";
                secondMonth = "May";
                thirdMonth = "June";
                break;
            case 3:
                firstMonth = "July";
                secondMonth = "August";
                thirdMonth = "September";
                break;
            case 4:
                firstMonth = "October";
                secondMonth = "November";
                thirdMonth = "December";
                break;
            default:
                break;
        }

        return (firstMonth, secondMonth, thirdMonth);
    }
}