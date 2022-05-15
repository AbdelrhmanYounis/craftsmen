namespace API.Controllers;

 //  [Authorize]
    public class CountryController : BaseApiController
    {
        public IUnitOfWork  _unitOfWork  ;
        public CountryController(IUnitOfWork  unitOfWork )
        {
            _unitOfWork = unitOfWork;

        }

       [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var Country=_unitOfWork.CountryRepository.GetByIdAsync(id);

           return  (Country != null) ? Ok(Country): BadRequest("This Country Not Found !!") ;
        }

        [HttpGet]
        public  IActionResult GetAll()
        {
            return Ok((_unitOfWork.CountryRepository.GetAll()));
        }

         [HttpDelete("{id}")]
        public async Task <ActionResult> Delete(int id)
        {
            var Country=_unitOfWork.CountryRepository.GetById(id);

            if (Country != null){

                _unitOfWork.CountryRepository.Delete(Country);
               return (await _unitOfWork.Complete())?Ok("deleted successfully ..."):BadRequest("Failed to delete Country !!");                 
            }

           return  BadRequest("Failed to delete Country !!") ;
        }

        [HttpPut]
        public  async Task <ActionResult> Update(Country country)
        {
            var Country=_unitOfWork.CountryRepository.GetById(country.Id);

            if (Country != null){

                Country.Name=Country.Name;

                _unitOfWork.CountryRepository.Update(Country);

               return (await _unitOfWork.Complete())? Ok("updated successfully ..."):BadRequest("Failed to update Country !!");
            }

           return  BadRequest("Failed to update Country !!") ;
           
        }
         [HttpPost]
        public  async Task <ActionResult> Create(Country country)
        {
                _unitOfWork.CountryRepository.Add(country);

               return (await _unitOfWork.Complete())? Ok("created successfully ..."):BadRequest("Failed to create Country !!");           
        }

    }
