using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using SnakeGameNS;

namespace Main.ProgramGUI {
  class ListedSnakeSettingsGUI : ListedSettingsGUI {

    private SnakeSettings snakeSettings;

    private NumericUpDown rowCountControl;
    private NumericUpDown columnCountControl;

    public ListedSnakeSettingsGUI(SnakeSettings snakeSettings, bool canEdit) : base(canEdit) {
      this.snakeSettings = snakeSettings;
      CreateItems();
      AddSettingItems();
    }

    public void CreateItems() {

      rowCountControl = new NumericUpDown();
      rowCountControl.Minimum = 10;
      rowCountControl.Maximum = 100;
      SettingItemGUI rowCount = new SettingItemGUI("Number of rows", 
                                                    snakeSettings.rowCount.ToString(),
                                                    rowCountControl);

      columnCountControl = new NumericUpDown();
      columnCountControl.Minimum = 10;
      columnCountControl.Maximum = 100;
      SettingItemGUI columnCount = new SettingItemGUI("Number of columns",
                                              snakeSettings.columnCount.ToString(),
                                              columnCountControl);
      settingItems.Add(rowCount);
      settingItems.Add(columnCount);
    }

    public void SaveSettings() {
      snakeSettings.rowCount = (int)rowCountControl.Value;
      snakeSettings.columnCount = (int)columnCountControl.Value;
    }
  }
}
