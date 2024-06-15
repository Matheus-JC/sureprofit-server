using SureProfit.Domain.Entities;
using SureProfit.Domain.Interfaces;

namespace SureProfit.Infra.Data.Repositories;

public class TagRepository(ApplicationDbContext context) : Repository<Tag>(context), ITagRepository;
