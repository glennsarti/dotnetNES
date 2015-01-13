﻿using System;
using System.IO;
using dotnetNES.Engine.Utilities;
using NUnit.Framework;

namespace dotnetNES.Tests.CPU
{
    public class CPUTests
    {
        [Test]
        public void Instruction_Test_No_Errors()
        {
            var engine = new Engine.Main.Engine(Path.Combine(Environment.CurrentDirectory, "TestRoms",
                    "instr_test-v5_official.nes"));

            var steps = 0;
            while (steps < 18029762 || engine.Processor.Memory.ReadValue(0x6000) == 0x80)
            {
                engine.NextStep();
                steps++;
            }

            var testOutput = new byte[19];
            var position = 0;
            for (var i = 0x6004; i < 0x6017; i++)
            {
                testOutput[position] = engine.Processor.Memory.ReadValue(i);
                position++;
            }

            var output = System.Text.Encoding.ASCII.GetString(testOutput);

            Assert.AreEqual("All 16 tests passed", output);
        }
    }
}