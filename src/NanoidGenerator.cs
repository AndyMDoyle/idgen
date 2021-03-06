//
// Author:
//   Aaron Bockover <aaron.bockover@gmail.com>
//
// Copyright 2018 Aaron Bockover.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

using Mono.Options;

namespace Idgen
{
    sealed class NanoidGenerator : IIdGenerator
    {
        int size = 21;
        string alphabet = "_~0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public string Command { get; } = "nanoid";
        public string CommandDescription { get; }
            = "Generate a Nano ID. See https://github.com/ai/nanoid";
        public string UsageArguments { get; }
        public OptionSet Options { get; }

        public NanoidGenerator ()
        {
            Options = new OptionSet {
                {
                    "s|size=",
                    "The ID will be {SIZE} characters in length (21 if unspecified).",
                    v => {
                        if (v != null && (!int.TryParse (v, out size) || size <= 0))
                            throw new Exception (
                                "SIZE must be a positive integer.");
                    }
                },
                {
                    "a|alphabet=",
                    $"Set the alphabet for -nanoid (default is {alphabet})",
                    v => alphabet = v
                }
            };
        }

        public string Generate (IEnumerable<string> args)
            => Nanoid.Nanoid.Generate (alphabet, size);
    }
}