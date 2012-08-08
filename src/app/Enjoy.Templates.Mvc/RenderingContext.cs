using System;
using System.Collections.Generic;
using System.IO;

namespace Enjoy.Mvc
{
    public class RenderingContext : IDisposable
    {
        private readonly Stack<TextWriter> writers;
        private readonly Stack<string> output;

        public RenderingContext(Stack<TextWriter> writers, Stack<string> output)
        {
            this.writers = writers;
            this.output = output;
            writers.Push(new StringWriter());
        }

        public void Dispose()
        {
            var writer = writers.Pop();
            output.Push(writer.ToString());
        }
    }
}