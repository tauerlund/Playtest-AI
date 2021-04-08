using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main.ProgramGUI {
  public partial class SettingItemGUI : Form {

    public Label Title { get; set; }
    public Label Value { get; set; }
    public Control EditControl { get; set; }
    public ToolTip toolTip { get; set; }
    
    public SettingItemGUI(string title, string value, Control editControl) : this(title, value, editControl, "") {
    }

    public SettingItemGUI(string title, string value, Control editControl, string toolTip) {

      Dock = DockStyle.Fill;

      AutoSize = true;

      Title = new Label();
      Title.Text = title;
      Title.AutoSize = true;
      Title.Anchor = AnchorStyles.Left;
      Title.Font = ConstantsGUI.FONT_SMALL;

      Value = new Label();
      Value.Text = value;
      Value.AutoSize = true;
      Value.Anchor = AnchorStyles.Left;
      Value.Font = ConstantsGUI.FONT_SMALL;

      EditControl = editControl;
      EditControl.Font = ConstantsGUI.FONT_SMALL;
      EditControl.Text = value;
      EditControl.TextChanged += OnTextChanged;
      EditControl.Anchor = Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
      //EditControl.Dock = DockStyle.Fill;

      this.toolTip = new ToolTip();
      this.toolTip.SetToolTip(Title, toolTip);
    }

    public void OnTextChanged(object sender, EventArgs e) {

      string c = e.ToString();

      if(c != Value.Text) {
        EditControl.Font = ConstantsGUI.FONT_SMALL_BOLD;
      }
      else {
        EditControl.Font = ConstantsGUI.FONT_SMALL;
      }
    }
  }
}
