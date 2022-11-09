using Homework6_LiudvynskyiV.S.Models;
using Homework6_LiudvynskyiV.S.Services;

var rootPath = @"D:\TasksData\ResidentsData.txt";
var resultPath = @"D:\TasksData\Result.txt";
var accountingService = new AccountingService(new Accounting(), rootPath, resultPath);

accountingService.ReadFile();
accountingService.WriteResult();