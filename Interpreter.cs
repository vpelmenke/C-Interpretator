using System;
using System.Collections.Generic;
using System.IO;

namespace KizhiPart1
{
    class Program
    {
        static void Main(string[] args)
        {
            Variable variable = new Variable();
            TextWriter text = new StringWriter();
            Interpreter interpreter = new Interpreter(text);
            interpreter.ExecuteLine("set a 5");
            interpreter.ExecuteLine("sub a 3");
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
            if (variable.GetCommand(str, variable) == 1)
                _writer.WriteLine("Переменная отсутствует в памяти");
            else if (variable.GetCommand(str, variable) == 2)
                Console.WriteLine(variable.GetVariable(str[1]));
            else if (variable.GetCommand(str, variable) == 3)
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
        public int GetCommand(String[] commandLine, Variable variable) // 1 - ошибка, отсутствует переменная | 2 - print | 3 - неизвестная команда
        {
            switch (commandLine[0])
            {
                case "set":
                    variable.SetVariable(commandLine[1], Convert.ToInt32(commandLine[2]));
                    break;
                case "sub":
                    if (!variable.CheckForContain(commandLine[1]))
                        return (1);
                    variable.SetVariable(commandLine[1], variable.GetVariable(commandLine[1]) + Convert.ToInt32(commandLine[2]));
                    break;
                case "rem":
                    if (!variable.CheckForContain(commandLine[1]))
                        return (1);
                    variable.RemoveVariable(commandLine[1]);
                    break;
                case "print":
                    if (!variable.CheckForContain(commandLine[1]))
                        return (1);
                    return (2);
                default:
                    return 3;
            }
            return (0);
        }
    }
}