using Project.Application.DTOs.Contract;

namespace Project.Application.Features.Interfaces
{
    public interface IContractService
    {
        Task<ContractDTO> CompleteLevel10(ContractLevel10 input);
        Task<ContractDTO> CompleteLevel11(ContractLevel11 input);
        Task<ContractDTO> CompleteLevel2(ContractLevel2 input);
        Task<ContractDTO> CompleteLevel3(ContractLevel3 input);
        Task<ContractDTO> CompleteLevel4(ContractLevel4 input);
        Task<ContractDTO> CompleteLevel5(ContractLevel5 input);
        Task<ContractDTO> CompleteLevel6(ContractLevel6 input);
        Task<ContractDTO> CompleteLevel7(ContractLevel7 input);
        Task<ContractDTO> CompleteLevel8(ContractLevel8 input);
        Task<ContractDTO> CompleteLevel9(ContractLevel9 input);
        Task<ContractDTO> CreateContract(CreateContract input);
        Task<ContractDTO> EditContract(EditContract input);
        Task<List<int>> GetAllMyContractBragainCodes();
        Task<ContractDTO> GetMyContract(int bargainCode);
        Task<string> GetMyContractHtml(int bargainCode);
    }
}
