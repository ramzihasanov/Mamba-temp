using Mamba.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Mambaa.ViewModels
{
    public class HomeViewModel
    {
        public List<Team> Teams { get; set; }
        public List<Posittion> Positations { get; set; }
    }
}
