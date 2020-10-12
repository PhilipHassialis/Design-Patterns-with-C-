using System;

namespace WeakEventPattern
{
    public class Button
    {
        public event EventHandler Clicked;
        public void Fire()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }

    public class Window
    {
        public Window(Button button)
        {
            button.Clicked += ButtonClicked;
            //WeakEventManager<Button, EventArgs>.AddHandler(button, "Clicked", ButtonClicked); .NET Framework only
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Button clicked (Window Handler)");
        }

        ~Window()
        {
            Console.WriteLine("Window finalised");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var btn = new Button();
            var window = new Window(btn);
            var windowRef = new WeakReference(window);
            btn.Fire();
            Console.WriteLine("Setting window to null");
            window = null;
            FireGC();
            Console.WriteLine($"Is the window alive after GC {windowRef.IsAlive}");
        }

        private static void FireGC()
        {
            Console.WriteLine("Starting GC");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Console.WriteLine("GC is done");
        }
    }
}
