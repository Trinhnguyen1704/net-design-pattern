using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using net_design_pattern.Domain.Models.DTOs;
using net_design_pattern.Persistence.Context;
using net_design_pattern.Repositories.Domain.Authorization;

namespace net_design_pattern.Persistence.Repositories.Authorization
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        // Dependency Injection by construtor
        public RoleRepository(AppDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }
        public List<RoleDto> GetRoles(int accountId)
        {
            try
            {
                var roles = _context.AccountHasRoles
                .Include(x => x.Role)
                .Where(x => x.AccountId == accountId && x.IsDeleted == false)
                .ToList();

                List<RoleDto> roleDtos = new List<RoleDto>();

                foreach (var role in roles)
                {
                    roleDtos.Add(_mapper.Map<RoleDto>(role.Role));
                }
                if(roleDtos.Capacity == 0)
                {
                    return null;
                }
                return roleDtos;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public bool CheckRole(int accountId)
        {
            List<RoleDto> roleDtos = GetRoles(accountId);
            var checkRole = false;
            foreach (var role in roleDtos)
            {
                if(role.Name.Contains("ADMIN"))
                {
                    checkRole = true;
                }
            }
            return checkRole;
        }
    }
}