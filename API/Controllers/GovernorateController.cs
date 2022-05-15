namespace API.Controllers;

 //  [Authorize]
    public class GovernorateController : BaseApiController
    {
        public IUnitOfWork  _unitOfWork  ;
        public GovernorateController(IUnitOfWork  unitOfWork )
        {
            _unitOfWork = unitOfWork;

        }

       [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var Governorate=_unitOfWork.GovernorateRepository.GetByIdAsync(id);

           return  (Governorate != null) ? Ok(Governorate): BadRequest("This Governorate Not Found !!") ;
        }
        [HttpGet("GetByParentId/{id}")]
        public async Task<IActionResult> GetByParentId(int id)
        {
            var Governorates=await _unitOfWork.GovernorateRepository.FindAllAsync(c=>c.CountryId==id);

           return  (Governorates != null) ? Ok(Governorates): BadRequest("This Governorates Not Found !!") ;
        }

        [HttpGet]
        public  IActionResult GetAll()
        {
            
            var x=_unitOfWork.GovernorateRepository.FindAllAsync(x=>x.Id>0,w=>w.Cities,x=>x.Country);
            foreach (var item in x.Result)
            {
                item.Country.Governorates=null;
            }
            return Ok(x);
        }

         [HttpDelete("{id}")]
        public async Task <ActionResult> Delete(int id)
        {
            var Governorate=_unitOfWork.GovernorateRepository.GetById(id);

            if (Governorate != null){

                _unitOfWork.GovernorateRepository.Delete(Governorate);
               return (await _unitOfWork.Complete())?Ok("deleted successfully ..."):BadRequest("Failed to delete Governorate !!");                 
            }

           return  BadRequest("Failed to delete Governorate !!") ;
        }

        [HttpPut]
        public  async Task <ActionResult> Update(Governorate governorate)
        {
            var Governorate=_unitOfWork.GovernorateRepository.GetById(governorate.Id);

            if (Governorate != null){

                Governorate.Name=Governorate.Name;

                _unitOfWork.GovernorateRepository.Update(Governorate);

               return (await _unitOfWork.Complete())? Ok("updated successfully ..."):BadRequest("Failed to update Governorate !!");
            }

           return  BadRequest("Failed to update Governorate !!") ;
           
        }
         [HttpPost]
        public  async Task <ActionResult> Create(Governorate Governorate)
        {
                _unitOfWork.GovernorateRepository.Add(Governorate);

               return (await _unitOfWork.Complete())? Ok("created successfully ..."):BadRequest("Failed to create Governorate !!");           
        }

    }
