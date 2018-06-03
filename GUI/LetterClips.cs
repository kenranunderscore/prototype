﻿namespace prototype.GUI
{
    using System.Collections.Generic;
    using SDL2;

    internal class LetterClips : ILetterClips
    {
        private static readonly IReadOnlyDictionary<char, SDL.SDL_Rect> Clips = new Dictionary<char, SDL.SDL_Rect>
        {
            [' '] = ClipFromIndices(0, 0),
            ['a'] = ClipFromIndices(1, 0),
            ['b'] = ClipFromIndices(2, 0),
            ['c'] = ClipFromIndices(3, 0),
            ['d'] = ClipFromIndices(4, 0),
            ['e'] = ClipFromIndices(5, 0),
            ['f'] = ClipFromIndices(6, 0),
            ['g'] = ClipFromIndices(7, 0),
            ['h'] = ClipFromIndices(8, 0),
            ['i'] = ClipFromIndices(9, 0),
            ['j'] = ClipFromIndices(10, 0),
            ['k'] = ClipFromIndices(11, 0),
            ['l'] = ClipFromIndices(12, 0),
            ['m'] = ClipFromIndices(13, 0),
            ['n'] = ClipFromIndices(14, 0),
            ['o'] = ClipFromIndices(15, 0),
            ['p'] = ClipFromIndices(16, 0),
            ['q'] = ClipFromIndices(17, 0),
            ['r'] = ClipFromIndices(18, 0),
            ['s'] = ClipFromIndices(19, 0),
            ['t'] = ClipFromIndices(0, 1),
            ['u'] = ClipFromIndices(1, 1),
            ['v'] = ClipFromIndices(2, 1),
            ['w'] = ClipFromIndices(3, 1),
            ['x'] = ClipFromIndices(4, 1),
            ['y'] = ClipFromIndices(5, 1),
            ['z'] = ClipFromIndices(6, 1),
            ['A'] = ClipFromIndices(7, 1),
            ['B'] = ClipFromIndices(8, 1),
            ['C'] = ClipFromIndices(9, 1),
            ['D'] = ClipFromIndices(10, 1),
            ['E'] = ClipFromIndices(11, 1),
            ['F'] = ClipFromIndices(12, 1),
            ['G'] = ClipFromIndices(13, 1),
            ['H'] = ClipFromIndices(14, 1),
            ['I'] = ClipFromIndices(15, 1),
            ['J'] = ClipFromIndices(16, 1),
            ['K'] = ClipFromIndices(17, 1),
            ['L'] = ClipFromIndices(18, 1),
            ['M'] = ClipFromIndices(19, 1),
            ['N'] = ClipFromIndices(0, 2),
            ['O'] = ClipFromIndices(1, 2),
            ['P'] = ClipFromIndices(2, 2),
            ['Q'] = ClipFromIndices(3, 2),
            ['R'] = ClipFromIndices(4, 2),
            ['S'] = ClipFromIndices(5, 2),
            ['T'] = ClipFromIndices(6, 2),
            ['U'] = ClipFromIndices(7, 2),
            ['V'] = ClipFromIndices(8, 2),
            ['W'] = ClipFromIndices(9, 2),
            ['X'] = ClipFromIndices(10, 2),
            ['Y'] = ClipFromIndices(11, 2),
            ['Z'] = ClipFromIndices(12, 2),
            ['1'] = ClipFromIndices(13, 2),
            ['2'] = ClipFromIndices(14, 2),
            ['3'] = ClipFromIndices(15, 2),
            ['4'] = ClipFromIndices(16, 2),
            ['5'] = ClipFromIndices(17, 2),
            ['6'] = ClipFromIndices(18, 2),
            ['7'] = ClipFromIndices(19, 2),
            ['8'] = ClipFromIndices(0, 3),
            ['9'] = ClipFromIndices(1, 3),
            ['0'] = ClipFromIndices(2, 3),
            [','] = ClipFromIndices(3, 3),
            ['.'] = ClipFromIndices(4, 3),
            [':'] = ClipFromIndices(5, 3),
            [';'] = ClipFromIndices(6, 3),
            ['\''] = ClipFromIndices(7, 3),
            ['"'] = ClipFromIndices(8, 3),
            ['!'] = ClipFromIndices(9, 3),
            ['?'] = ClipFromIndices(10, 3),
            ['@'] = ClipFromIndices(11, 3),
            ['~'] = ClipFromIndices(12, 3),
            ['#'] = ClipFromIndices(13, 3),
            ['$'] = ClipFromIndices(14, 3),
            ['%'] = ClipFromIndices(15, 3),
            ['^'] = ClipFromIndices(16, 3),
            ['&'] = ClipFromIndices(17, 3),
            ['*'] = ClipFromIndices(18, 3),
            ['('] = ClipFromIndices(19, 3),
            [')'] = ClipFromIndices(0, 4),
            ['-'] = ClipFromIndices(1, 4),
            ['_'] = ClipFromIndices(2, 4),
            ['='] = ClipFromIndices(3, 4),
            ['+'] = ClipFromIndices(4, 4),
            ['['] = ClipFromIndices(5, 4),
            ['{'] = ClipFromIndices(6, 4),
            ['}'] = ClipFromIndices(7, 4),
            [']'] = ClipFromIndices(8, 4),
            ['\\'] = ClipFromIndices(9, 4),
            ['|'] = ClipFromIndices(10, 4),
            ['<'] = ClipFromIndices(11, 4),
            ['>'] = ClipFromIndices(12, 4),
            ['/'] = ClipFromIndices(13, 4),
            ['ä'] = ClipFromIndices(14, 4),
            ['ö'] = ClipFromIndices(15, 4),
            ['ü'] = ClipFromIndices(16, 4),
            ['ß'] = ClipFromIndices(17, 4),
            ['Ä'] = ClipFromIndices(18, 4),
            ['Ö'] = ClipFromIndices(19, 4),
            ['Ü'] = ClipFromIndices(0, 5),
            ['°'] = ClipFromIndices(1, 5),
        };

        private static SDL.SDL_Rect ClipFromIndices(int x, int y) =>
            new SDL.SDL_Rect
            {
                x = x * Defaults.LetterWidth,
                y = y * Defaults.LetterHeight,
                w = Defaults.LetterWidth,
                h = Defaults.LetterHeight
            };

        public SDL.SDL_Rect GetClip(char c) => Clips[c];
    }
}
