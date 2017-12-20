using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControllerGamer.Libraries.SimInput
{
    /// <summary>
    /// This is more an helper class than a built-in library.
    /// Launchpad can also be wrapped outside in the profile, like in namespace GameProfile.
    /// </summary>
    public class Launchpad
    {
        public MIDI Midi = null;

        public Launchpad(MIDI _midi)
        {
            Midi = _midi;

            // rewire output
            Midi.seqr.ChannelMessagePlayed -= Midi.OnChannelMessagePlayed;
            Midi.seqr.ChannelMessagePlayed += OnChannelMessagePlayed;

            Midi.Send(240, 0, 32, 41, 2, 16, 44, 3, 247);       // programmer mode.

        }

        private int[] note_button_map = {
            91, 92, 93, 94, 95, 96, 97, 98, 11, 12, 13, 14, 21, 22, 23, 24, 31, 32, 33, 34, 41, 42, 43, 44,
            51, 52, 53, 54, 61, 62, 63, 64, 71, 72, 73, 74, 81, 82, 83, 84, 15, 16, 17, 18, 25, 26, 27, 28,
            35, 36, 37, 38, 45, 46, 47, 48, 55, 56, 57, 58, 65, 66, 67, 68, 75, 76, 77, 78, 85, 86, 87, 88,
            89, 79, 69, 59, 49, 39, 29, 19, 80, 70, 60, 50, 40, 30, 20, 10, 1, 2, 3, 4, 5, 6, 7, 8
        };
        const int note_button_offset = 28;
        const int note_button_size = 96;
        // End generate block


        // Note<>Button conversion
        private int note_to_button(int note)
        {
            if (note < note_button_offset || note > (note_button_size + note_button_offset)) return 0;
            return note_button_map[note - note_button_offset];
        }

        private int button_to_note(int button)
        {
            for (int i = 0; i < note_button_size + note_button_offset; i++)
                if (note_button_map[i] == button) return i + note_button_offset;
            return 0;
        }


        /// <summary>
        /// Set the color of a key/button.
        /// </summary>
        /// <param name="channel">0-7</param>
        /// <param name="note">8x8 keys + 8x4 buttons </param>
        /// <param name="color">Color 0-127</param>
        public void SetColor(int channel, int note, int color)
        {
            if (Midi.DeviceID != -1)
            {
                ChannelMessageBuilder cmb = new ChannelMessageBuilder
                {
                    Command = ChannelCommand.NoteOn,
                    MidiChannel = channel,
                    Data1 = note,
                    Data2 = color
                };
                cmb.Build();
                Midi.Device.Send(cmb.Result);
            }
        }

        public void SetColor(int note, int color)
        {
            SetColor(0, note, color);
        }


        public void UnsetColor(int channel, int note)
        {
            SetColor(0, note, 0);
        }

        public void UnsetColor(int note)
        {
            UnsetColor(0, note);
        }

        public void SetRowColor(int channel, int row, int color)
        {
            for (int i = 0; i < 10; i++)
            {
                SetColor(channel, row * 10 + i, color);
            }
        }

        public void SetRowColor(int row, int color)
        {
            SetRowColor(0, row, color);
        }

        public void UnsetRowColor(int channel, int row)
        {
            SetRowColor(channel, row, 0);
        }

        public void UnsetRowColor(int row)
        {
            SetRowColor(0, row, 0);
        }

        public void SetColumnColor(int channel, int col, int color)
        {
            for (int i = 0; i < 10; i++)
            {
                SetColor(channel, i * 10 + col, color);
            }
        }

        public void SetColumnColor(int col, int color)
        {
            SetColumnColor(0, col, color);
        }
        public void UnsetColumnColor(int channel, int col)
        {
            SetColumnColor(channel, col, 0);
        }

        public void UnsetColumnColor(int col)
        {
            SetColumnColor(0, col, 0);
        }

        protected void OnChannelMessagePlayed(object sender, ChannelMessageEventArgs e)
        {
            if (Midi.DeviceID != -1)
            {
                ChannelMessageBuilder cmb = new ChannelMessageBuilder(e.Message);
                cmb.Command = e.Message.Command;
                cmb.Data1 = note_to_button(cmb.Data1);
                cmb.Build();
                Midi.Device.Send(cmb.Result);
            }
        }

    }

}
