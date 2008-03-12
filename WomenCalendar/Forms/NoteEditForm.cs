﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WomenCalendar
{
    public partial class NoteEditForm : ModalBaseForm
    {
        public NoteEditForm()
        {
            InitializeComponent();
        }

        public NoteEditForm(string editText)
            : this()
        {
            txtNote.Text = (!editText.Contains("\r\n")) ? editText.Replace("\n", "\r\n") : editText;
        }

        public string NoteText
        {
            get
            {
                return txtNote.Text;
            }
        }
    }
}
