using System;
using System.Collections.Generic;
using System.IO;

// ReSharper disable once IdentifierTypo
namespace KizhiPart1
{
    class Program
    {
        // ReSharper disable once UnusedParameter.Local
        static void Main(string[] args)
        {
            Variable variable = new Variable();
            TextWriter text = new StringWriter();
            Interpreter interpreter = new Interpreter(text);
            interpreter.ExecuteLine("set a 5");
            interpreter.ExecuteLine("sub a 3");
            interpreter.ExecuteLine("sub a 8");
            interpreter.ExecuteLine("print a");
        }
    }
    public class Interpreter
    {
        private TextWriter _writer;
        private Variable variable = new Variable();

        public Interpreter(TextWriter writer)
        {
            _writer = writer;
        }

        public void ExecuteLine(string command)
        {
            String[] str = command.Split(' ');
            _writer.WriteLine(command);
            int resultOfCommand = variable.GetCommand(str, variable);
            if (resultOfCommand == 1)
                _writer.WriteLine("Переменная отсутствует в памяти");
            else if (resultOfCommand == 2)
                Console.WriteLine(variable.GetVariable(str[1]));
            else if (resultOfCommand == 3)
                _writer.WriteLine("Wrong command");
         }
    }

    public class Variable
    {
        Dictionary<string, int> variables;
        public Variable()
        {
            variables = new Dictionary<string, int>();
        }

        public int GetVariable(string name)
        {
            return variables[name];
        }

        public void SetVariable(string name, int value)
        {
            if (variables.ContainsKey(name))
                variables[name] = value;
            else
                variables.Add(name, value);
        }

        public bool CheckForContain(string name)
        {
            return (variables.ContainsKey(name));
        }

        public void RemoveVariable(string name)
        {
            variables.Remove(name);
        }
        /// <summary>
        /// Обработка введенных команд. Обозначения возвращаемых значений : 1 - ошибка, отсутствует переменная, 2 - печать, 3 - неизвестная команда или ошибка ввода
        /// </summary>
        public int GetCommand(String[] commandLine, Variable variable)
        {
            string varName = commandLine[1];
            switch (commandLine[0])
            {
                case "set":
                    if (int.TryParse(commandLine[2], out int number))
                        variable.SetVariable(varName, number);
                    else
                        return (3);
                    break;
                case "sub":
                    if (!variable.CheckForContain(varName))
                        return (1);
                    if (int.TryParse(commandLine[2], out  number))
                    {
                        if (variable.GetVariable(varName) < number)
                            return (3);

                        variable.SetVariable(varName, variable.GetVariable(varName) - number);
                    }
                    break;
                case "rem":
                    if (!variable.CheckForContain(varName))
                        return (1);
                    variable.RemoveVariable(varName);
                    break;
                case "print":
                    if (!variable.CheckForContain(varName))
                        return (1);
                    return (2);
                default:
                    return 3;
            }
            return (0);
        }
    }
}