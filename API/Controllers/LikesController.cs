using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class LikesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public LikesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("{username}")]
        public async Task<ActionResult> AddLike(string username)
        {
            var sourceUserId = User.GetUserId();
            var likedUser = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);
            var sourceUser = await _unitOfWork.LikesRepository.GetUserWithLikes(sourceUserId);

            if (likedUser == null) return NotFound();

            if (sourceUser.UserName == username) return BadRequest("You cannot like yourself");

            var userLike = await _unitOfWork.LikesRepository.GetUserLike(sourceUserId, likedUser.Id);

            if (userLike != null) return BadRequest("You already like this user");

            userLike = new UserLike
            {
                SourceUserId = sourceUserId,
                LikedUserId = likedUser.Id
            };

            sourceUser.LikedUsers.Add(userLike);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to like user");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeDto>>> GetUserLikes([FromQuery] LikesParams likesParams)
        {
            int UserId=User.GetUserId();
            likesParams.UserId = UserId;
            var users = await _unitOfWork.LikesRepository.GetUserLikes(likesParams);

            foreach (var item in users)
            {
                if(await _unitOfWork.LikesRepository.GetUserLike(UserId,item.Id)==null)
                    item.IsLiked=false;

                else
                item.IsLiked=true;
            }

            Response.AddPaginationHeader(users.CurrentPage,
                users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(users);
        }
         [HttpGet("IsLiked/{id}")]
        public async Task<Boolean> IsLiked(int id)
        {
          return  (await _unitOfWork.LikesRepository.GetUserLike(User.GetUserId(),id)==null)?
                   false: true;
        }
        [HttpDelete("Delete/{id}")]
        public async Task <ActionResult> Delete(int id)
        {
            var Result=await _unitOfWork.LikesRepository.GetUserLike(User.GetUserId(),id);

            if (Result != null){

                _unitOfWork.LikesRepository.Delete(Result);
                
               return (await _unitOfWork.Complete())?
               Ok():BadRequest("Failed to remove this Like !!");                 
            }

           return  BadRequest("Failed to remove this Like !!") ;
        }
    }
}