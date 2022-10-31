﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yukar.Common;

namespace Json2BakinPlugin
{
    public partial class TestForm : Form
    {
        Guid TestChunkID = new Guid("88431A79-F9A5-499F-96ED-4CAEC55F59D8");
        private TestChunk testChunk;
        private Catalog catalog;
        private string originalText;

        public class TestChunk : Yukar.Common.Rom.IChunk
        {
            public int saveCount;

            public void load(BinaryReader reader)
            {
                saveCount = reader.ReadInt32();
            }

            public void save(BinaryWriter writer)
            {
                writer.Write(saveCount);
            }
        }

        public TestForm()
        {
            InitializeComponent();
        }

        public TestForm(Yukar.Common.Catalog catalog)
        {
            this.catalog = catalog;

            InitializeComponent();

            originalText = label1.Text;

            // Load ExtraChunk
            testChunk = new TestChunk();
            var entries = catalog.getFilteredExtraChunkList(TestChunkID);
            if (entries.Count > 0)
                entries[0].readChunk(testChunk);
            testChunk.saveCount++;
            label1.Text = string.Format(originalText, testChunk.saveCount);
        }

        private void TestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save ExtraChunk
            Yukar.Common.Rom.ExtraChunk rom = null;
            var entries = catalog.getFilteredExtraChunkList(TestChunkID);
            if (entries.Count == 0)
            {
                rom = new Yukar.Common.Rom.ExtraChunk();
                rom.id = TestChunkID;
                catalog.addItem(rom);
            }
            else
            {
                rom = entries[0];
            }
            rom.writeChunk(testChunk);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

    }
}
