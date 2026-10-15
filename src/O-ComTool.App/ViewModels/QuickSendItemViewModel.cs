using CommunityToolkit.Mvvm.ComponentModel;

namespace O_ComTool.App.ViewModels
{
    /**
     * @brief 快捷发送列表中的一行
     */
    public partial class QuickSendItemViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _index = 1;

        [ObservableProperty]
        private string _name = "Name";

        [ObservableProperty]
        private string _data = "Data";

        public string IndexText => Index.ToString("000");

        partial void OnIndexChanged(int value)
        {
            OnPropertyChanged(nameof(IndexText));
        }
    }
}
