using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Test.Domain.Entities;
using Test.Domain.Interfaces;
using Test.Domain.Validations;
using Test.Domain.ViewModel;

namespace Test.Domain.Services
{
    public class EmployeeService : IGenericService<Employee>
    {
        private readonly IAsyncRepository<Employee> _repository;
        public IFormFile File { get; set; }
        public string PathPhoto { get; set; }
        public EmployeeService(IAsyncRepository<Employee> repository)
        {
            _repository = repository;
        }

        public async Task<Result> AddAsync(Employee entity)
        {
            var result = new Result();
            try
            {
                if (await AnyAsync(a=> a.Name.ToLower() == entity.Name.ToLower() && a.LastName.ToLower() == entity.LastName.ToLower() ))
                    return new Result() { message = $"Ya existe el empleado {entity.Name} {entity.LastName}.", _return = true };

                if (!Directory.Exists(PathPhoto))
                {
                    Directory.CreateDirectory(PathPhoto);
                }

                var fileName = File.FileName.Split('.');
                var path = $@"{PathPhoto}/{entity.Id}.{fileName[1]}";

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await File.CopyToAsync(stream);
                }
                entity.Photo = $@"Upload/PhotoEmployee/{entity.Id}.{fileName[1]}";
                await _repository.AddAsync(entity);
                result.message = "Se ha registrado con exito.";
                result.result = entity;
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
                result.result = true; 
            }
            return result;
        }

        public Employee GetEmployee(EmployeeViewModel viewModel, string path = null)
        {
            var emp = new Employee() 
            {
                Email = viewModel.Email,
                HiringDate = viewModel.HiringDate,
                Id = viewModel.Id,
                LastName = viewModel.LastName,
                Name = viewModel.Name,
                Phone = viewModel.Phone,
                Photo = viewModel.Photo
            };
            File = viewModel.File;
            PathPhoto = path;
            return emp;
        }

        public async Task<Result> DeleteAsync(Guid Id)
        {
            var result = new Result();
            try
            {
                var entity = await _repository.GetExpressionAsync(e => e.Id == Id);
                await _repository.DeleteAsync(entity);
                result.message = "Se ha eliminado con exito.";
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
                result.result = true;
            }
            return result;
        }

        public async Task<Result> GetAllAsync()
        {
            var result = new Result();
            try
            {
                var d = await _repository.ListAllAsync();
                result.data = d.AsQueryable();
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
                result.result = true;
            }
            return result;
        }
        public IEnumerable<Employee> GetAllEmpAsync()
        {
            try
            {
                var d = _repository.ListAllAsync().Result;
                return d.AsEnumerable(); //new DataTableResponse() { Data =  listEmp.ToArray(), RecordsTotal = listEmp.Count, RecordsFiltered = 10 };
            }
            catch (Exception ex)
            {
               
            }
            return null;//new DataTableResponse();
        }
        public async Task<Result> GetAsync(Guid Id)
        {
            var result = new Result();
            try
            {
                result.result = await _repository.GetExpressionAsync(e=> e.Id == Id);
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
                result.result = true;
            }
            return result;
        }

        public async Task<Result> UpdateAsync(Employee entity)
        {
            var result = new Result();
            try
            {
                if(File != null)
                {
                    if (!Directory.Exists(PathPhoto))
                    {
                        Directory.CreateDirectory(PathPhoto);
                    }

                    var fileName = File.FileName.Split('.');
                    var path = $@"{PathPhoto}/{entity.Id}.{fileName[1]}";

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await File.CopyToAsync(stream);
                    }
                    entity.Photo = $@"Upload/PhotoEmployee/{entity.Id}.{fileName[1]}";
                }
                await _repository.UpdateAsync(entity);
                result.message = "Se ha actualizado con exito.";
                result.result = entity;
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
                result.result = true;
            }
            return result;
        }

        public async Task<Result> Search(string value)
        {
            var result = new Result();
            try
            {
                var entities = await _repository.ListAllAsync();
                result.result = entities.Where(w => w.Name.Contains(value)
                || w.LastName.Contains(value)
                || w.Email.ToString().Contains(value)
                || w.HiringDate.ToString().Contains(value))
                    .ToList();
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
                result.result = true;
            }
            return result;
        }

        async Task<bool> AnyAsync(Expression<Func<Employee, bool>> predicate) => await _repository.AnyExpressionAsync(predicate);
    }
}
