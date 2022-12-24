using Homework12_LiudvynskyiV.S.Services;

var rootPath = @"";

var fileHandler = new FileHandler(rootPath);
var paydeskSimulator = new PaydeskSimulator(fileHandler);
paydeskSimulator.StartPaydesksOperations();