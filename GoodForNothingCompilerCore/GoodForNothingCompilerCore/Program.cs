﻿using System;
using System.IO;

namespace GoodForNothingCompilerCore
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: gfn.exe program.gfn");
                return;
            }

            var path = args[0];
            var moduleName = Path.GetFileNameWithoutExtension(path) + ".exe";

            Scanner scanner;
            using (var input = File.OpenText(path))
            {
                scanner = new Scanner(input);
            }
            var parser = new Parser(scanner.Tokens);
            parser.Parse();

            var codeGen = new CodeGen(parser.Result, moduleName);
            codeGen.Compile();
            Console.WriteLine("Successfully compiled to " + moduleName);
        }
    }
}
