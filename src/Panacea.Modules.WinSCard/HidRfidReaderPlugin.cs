using Panacea.Modularity.RfidReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinscardDotNet;

namespace Panacea.Modules.WinSCard
{
    public class HidRfidReaderPlugin : IRfidReaderPlugin
    {
        WinsCardReaderMonitor _monitor;
        public event EventHandler<string> CardConnected;
        public event EventHandler<string> CardDisconnected;

        public HidRfidReaderPlugin()
        {
            _monitor = new WinsCardReaderMonitor();
        }

        public Task BeginInit()
        {
            _monitor.CardConnected += _reader_CardConnected;
            _monitor.CardDisconnected += _reader_CardDisconnected;
            return Task.CompletedTask;
        }

        private void _reader_CardDisconnected(object sender, CardConnectedEventArgs e)
        {
            CardDisconnected?.Invoke(this, e.CardId);
        }

        private void _reader_CardConnected(object sender, CardConnectedEventArgs e)
        {
            CardConnected?.Invoke(this, e.CardId);
        }

        public void Dispose()
        {
            _monitor.Dispose();
        }

        public Task EndInit()
        {
            _monitor.Start();
            return Task.CompletedTask;
        }

        public Task Shutdown()
        {
            _monitor.Stop();
            return Task.CompletedTask;
        }

        public void SimulateCardTap(string s)
        {
            CardConnected?.Invoke(this, s);
        }
    }
}
