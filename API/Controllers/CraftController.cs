namespace API.Controllers;

 //  [Authorize]
    public class CraftController : BaseApiController
    {
        public IUnitOfWork  _unitOfWork  ;
        public CraftController(IUnitOfWork  unitOfWork )
        {
            _unitOfWork = unitOfWork;

        }

       [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var Craft=_unitOfWork.CraftRepository.GetByIdAsync(id);

           return  (Craft != null) ? Ok(Craft): BadRequest("This Craft Not Found !!") ;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok((await _unitOfWork.CraftRepository.GetAllAsync()));
        }

         [HttpDelete("{id}")]
        public async Task <ActionResult> Delete(int id)
        {
            var Craft=_unitOfWork.CraftRepository.GetById(id);

            if (Craft != null){

                _unitOfWork.CraftRepository.Delete(Craft);
               return (await _unitOfWork.Complete())?Ok("deleted successfully ..."):BadRequest("Failed to delete Craft !!");                 
            }

           return  BadRequest("Failed to delete Craft !!") ;
        }

        [HttpPut]
        public  async Task <ActionResult> Update(Craft craft)
        {
            var Craft=_unitOfWork.CraftRepository.GetById(craft.Id);

            if (Craft != null){

                Craft.Name=Craft.Name;

                _unitOfWork.CraftRepository.Update(Craft);

               return (await _unitOfWork.Complete())? Ok("updated successfully ..."):BadRequest("Failed to update Craft !!");
            }

           return  BadRequest("Failed to update Craft !!") ;
           
        }
         [HttpPost]
        public  async Task <ActionResult> Create(Craft Craft)
        {
                _unitOfWork.CraftRepository.Add(Craft);

               return (await _unitOfWork.Complete())? Ok("created successfully ..."):BadRequest("Failed to create Craft !!");           
        }

    }
