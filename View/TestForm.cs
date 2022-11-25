using System;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using Json2BakinPlugin.Controller;

namespace Json2BakinPlugin
{
    public partial class TestForm : Form
    {
        #region Variables
        private Json2BakinPluginController controller = new Json2BakinPluginController();
        #endregion

        #region Initialize
        public TestForm()
        {
            InitializeComponent();
            jsonFolderTextBox.DataBindings.Add("Text", controller, "JsonFolder");
            bakinFolderTextBox.DataBindings.Add("Text", controller, "BakinFolder");
            alertCommentCheckBox.DataBindings.Add("Checked", controller, "IsAddReviceComment");
            nonConvertCommentCheckBox.DataBindings.Add("Checked", controller, "IsAddNonConvertedComment");
            logTextBox.DataBindings.Add("Text", controller, "LogText");
            
            jsonFolderTextBox.DataBindings.Add("Enabled", controller, "UiEnabled");
            bakinFolderTextBox.DataBindings.Add("Enabled", controller, "UiEnabled");
            alertCommentCheckBox.DataBindings.Add("Enabled", controller, "UiEnabled");
            nonConvertCommentCheckBox.DataBindings.Add("Enabled", controller, "UiEnabled");
            transformButton.DataBindings.Add("Enabled", controller, "UiEnabled");
            jsonSelector.DataBindings.Add("Enabled", controller, "UiEnabled");
            bakinSelector.DataBindings.Add("Enabled", controller, "UiEnabled");

            fileNameMapNameCheck.DataBindings.Add("Checked", controller, "IsMapName");
            fileNameMapNumCheck.DataBindings.Add("Checked", controller, "IsMapNumber");
        }
        #endregion

        #region Privates
        private void TestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void jsonSelector_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                controller.JsonFolder = dialog.FileName;
            }
        }

        private void bakinSelector_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                controller.BakinFolder = dialog.FileName;
            }
        }

        private void nonConvertCommentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            controller.IsAddNonConvertedComment = nonConvertCommentCheckBox.Checked;
        }

        private void alertCommentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            controller.IsAddReviceComment = alertCommentCheckBox.Checked;
        }

        private void fileNameMapNumCheck_CheckedChanged(object sender, EventArgs e)
        {
            controller.IsMapNumber = fileNameMapNumCheck.Checked;
            if (!controller.IsMapNumber && !controller.IsMapName)
            {
                fileNameMapNumCheck.Checked = true;
                controller.IsMapNumber = fileNameMapNumCheck.Checked;
            }
        }

        private void fileNameMapNameCheck_CheckedChanged(object sender, EventArgs e)
        {
            controller.IsMapName = fileNameMapNameCheck.Checked;
            if (!controller.IsMapNumber && !controller.IsMapName)
            {
                fileNameMapNumCheck.Checked = true;
                controller.IsMapNumber = fileNameMapNumCheck.Checked;
            }
        }

        private void transformButton_Click(object sender, EventArgs e)
        {
            controller.UiEnabled = false;
            controller.Convert();
            controller.UiEnabled = true;
        }
        #endregion

    }
}
