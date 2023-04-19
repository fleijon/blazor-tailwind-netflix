using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace blazor_tailwind_netflix.Client;

public class TrackableValue<T> : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private T? _value;
    public T? Value {
        get => _value;
        set
        {
            _value = value;
            NotifyPropertyChanged();
        }
    }

    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}