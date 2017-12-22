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
        public int Channel = 0;

        public Launchpad(MIDI _midi)
        {
            Midi = _midi;

            // rewire output
            Midi.seqr.ChannelMessagePlayed -= Midi.OnChannelMessagePlayed;
            Midi.seqr.ChannelMessagePlayed += OnChannelMessagePlayed;

            Midi.Send(240, 0, 32, 41, 2, 16, 44, 3, 247);       // programmer mode.

        }

        #region Midifile Note to Launchpad Note

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
        #endregion


        private void SendSysEx(params object[] content)
        {
            // sysex begin
            List<byte> msg = new List<byte>() { 0xF0, 0x00, 0x20, 0x29, 0x02, 0x10 };
            // content
            foreach (var c in content)
            {
                msg.Add((byte)c);
            }
            // sysex end
            msg.Add(247);
        }

        /// <summary>
        /// Set the color of a key/button.
        /// </summary>
        /// <param name="channel">0-7</param>
        /// <param name="note">8x8 keys + 8x4 buttons </param>
        /// <param name="color">Color 0-127</param>
        public void SetColor(int note, int color)
        {
            if (Midi.DeviceID != -1 && note>=0)
            {
                ChannelMessageBuilder cmb = new ChannelMessageBuilder
                {
                    Command = ChannelCommand.NoteOn,
                    MidiChannel = Channel,
                    Data1 = note,
                    Data2 = color
                };
                cmb.Build();
                Midi.Device.Send(cmb.Result);
            }
        }

        public void SetColor(int note, System.Drawing.Color color)
        {
            SetColor(note, color.R, color.G, color.B);
        }

        public void SetColor(int note, int r, int g, int b)
        {
            SendSysEx(11, note, r, g, b);
        }

        public void PulseColor(int note, int color)
        {
            SendSysEx(40, note, color);
        }
        public void StopPulseColor(int note)
        {
            SendSysEx(40, note, 0);
        }

        public void SetFlashColor(int note, int color)
        {
            SendSysEx(35, note, color);
        }
        public void UnsetFlashColor(int note)
        {
            SendSysEx(35, note, 0);
        }


        public void SetPadColor(int color)
        {
            SendSysEx(14, (byte)color);
        }

        public void ClearPad()
        {
            SendSysEx(14, 0);
        }

         public void UnsetColor(int note)
        {
            SetColor(note, 0);
        }

        public void SetRowColor(int row, int color)
        {
            for (int i = 0; i < 10; i++)
            {
                SetColor(row * 10 + i, color);
            }
        }


        public void UnsetRowColor(int row)
        {
            SetRowColor(row, 0);
        }


        public void SetColumnColor(int col, int color)
        {
            for (int i = 0; i < 10; i++)
            {
                SetColor(i * 10 + col, color);
            }
        }

        public void UnsetColumnColor(int col)
        {
            SetColumnColor(col, 0);
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
