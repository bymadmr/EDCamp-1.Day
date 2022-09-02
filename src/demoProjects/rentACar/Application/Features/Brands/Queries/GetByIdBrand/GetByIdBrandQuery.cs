using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetByIdBrand
{
    public class GetByIdBrandQuery:IRequest<BrandGetByIdDto>
    {
        public int Id { get; set; }
        public class GetListBrandQueryHandler : IRequestHandler<GetByIdBrandQuery, BrandGetByIdDto>
        {

            IBrandRepository _brandRepository;
            IMapper _mapper;
            BrandBusinessRules _brandBusinessRules;
            public GetListBrandQueryHandler(IMapper mapper, IBrandRepository brandRepository, BrandBusinessRules brandBusinessRules)
            {
                _mapper = mapper;
                _brandRepository = brandRepository;
                _brandBusinessRules = brandBusinessRules;
            }
            public async Task<BrandGetByIdDto> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
            {
                Brand? brand = await _brandRepository.GetAsync(x => x.Id == request.Id);
                _brandBusinessRules.LanguageShouldExistWhenRequested(brand);
                BrandGetByIdDto result = _mapper.Map<BrandGetByIdDto>(brand);
                return result;
            }
        }
    }
}
