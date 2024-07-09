using DI.Interface;
using System.Threading.Tasks;

namespace DI.Service
{
    public class Bar : Base, IBar
    {
        public Task InvokeAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
