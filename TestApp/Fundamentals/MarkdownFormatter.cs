using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
    // Markdown syntax
    // https://www.markdownguide.org/basic-syntax/

    public class MarkdownFormatter
    {
        public string FormatAsBold(string content)
        {
            Validate(content);

            return $"**{content}**";
        }

        public string FormatAsItalic(string content)
        {
            Validate(content);

            return $"_{content}_";
        }

        private static void Validate(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new FormatException();
        }
    }
}
