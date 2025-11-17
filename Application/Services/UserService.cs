using Application.DTOs.UserDto;
using AutoMapper;
using Domain.Entities;
using Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ReadUserDto>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            return users.Select(u => new ReadUserDto
            {
                Id = u.Id,
                Email = u.Email,
                Role = u.Role
            });
        }

        public async Task<ReadUserDto?> GetByIdAsync(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return null;

            return new ReadUserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<ReadUserDto?> GetByEmailAsync(string email)
        {
            var user = await _repository.GetByEmailAsync(email);
            if (user == null) return null;

            return new ReadUserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<ReadUserDto> CreateAsync(CreateUserDto dto)
        {
            var user = new Users
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                PassWord = dto.PassWord, 
                Role = dto.Role ?? "user"
            };

            await _repository.AddAsync(user);

            return new ReadUserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task UpdateAsync(UpdateUserDto dto)
        {
            var user = new Users
            {
                Id = dto.Id,
                Email = dto.Email,
                PassWord = dto.PassWord,
                Role = dto.Role
            };

            await _repository.UpdateAsync(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user != null)
                await _repository.DeleteAsync(user);
        }


    }
}
