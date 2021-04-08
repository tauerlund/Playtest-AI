using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using SnakeGameNS;

namespace SnakeAI {
  class ListedSnakeSettingsGUI : ListedSettingsGUI {

    private SnakeAISettings snakeAISettings;

    private NumericUpDown rowCountControl;
    private NumericUpDown columnCountControl;

    public ListedSnakeSettingsGUI(SnakeAISettings snakeAISettings, bool canEdit) : base(canEdit) {
      this.snakeAISettings = snakeAISettings;
      CreateItems();
      AddSettingItems();
    }

    public void CreateItems() {

      rowCountControl = new NumericUpDown();
      rowCountControl.Minimum = 10;
      rowCountControl.Maximum = 100;
      SettingItemGUI rowCount = new SettingItemGUI("Number of rows", 
                                                    snakeAISettings.SnakeSettings.RowCount.ToString(),
                                                    rowCountControl);

      columnCountControl = new NumericUpDown();
      columnCountControl.Minimum = 10;
      columnCountControl.Maximum = 100;
      SettingItemGUI columnCount = new SettingItemGUI("Number of columns",
                                              snakeAISettings.SnakeSettings.ColumnCount.ToString(),
                                              columnCountControl);
      settingItems.Add(rowCount);
      settingItems.Add(columnCount);
    }

    public void SaveSettings() {
      snakeAISettings.SnakeSettings.UpdateSettings((int)rowCountControl.Value,
                                                   (int)columnCountControl.Value);
    }
  }
}
