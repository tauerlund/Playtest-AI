using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SnakeAI {
  public class ListedSettingsGUI : TableLayoutPanel {

    public List<SettingItemGUI> settingItems;
    public bool CanEdit;

    public ListedSettingsGUI(bool canEdit) {

      CanEdit = canEdit;
      settingItems = new List<SettingItemGUI>();

      Padding padding = new Padding();
      padding.All = 10;

      Margin = padding;

      AutoSize = true;
      AutoSizeMode = AutoSizeMode.GrowAndShrink;
      Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

      BackColor = ConstantsGUI.SETTINGSBOX_BACKGROUND_COLOR;
      CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

      ColumnCount = 2;
      ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
      ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

    }

    public void ResetFontType() {
      foreach (SettingItemGUI setting in settingItems) {
        setting.Value.Text = setting.EditControl.Text;
        setting.EditControl.Font = ConstantsGUI.FONT_SMALL;
      }
    }

    public void AddSettingItems() {
      RowCount = settingItems.Count;

      if (CanEdit) {
        for (int i = 0; i < RowCount; i++) {
          Controls.Add(settingItems[i].Title, 0, i);
          Controls.Add(settingItems[i].EditControl, 1, i);
        }
      }

      else {
        for (int i = 0; i < RowCount; i++) {
          Controls.Add(settingItems[i].Title, 0, i);
          Controls.Add(settingItems[i].Value, 1, i);
        }
      }
    }
  }
}
