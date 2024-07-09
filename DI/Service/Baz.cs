using DI.Interface;
using System.Threading.Tasks;

namespace DI.Service
{
    public class Baz : Base, IBaz, IBar
    {
        public Task InvokeAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
