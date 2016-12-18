namespace RoboBank.Customer.Application.Isolated.Adapters
{
    public class Mapper : Ports.IMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return AutoMapper.Mapper.Map<TSource, TDestination>(source);
        }
    }
}
