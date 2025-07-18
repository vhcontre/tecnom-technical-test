using System.Threading.Tasks;
using technical_tests_backend_ssr.Models;

public interface ILeadService
{
    Task<Lead> CreateLeadAsync(Lead lead);
    Task<Lead?> GetLeadByIdAsync(int id);
}
