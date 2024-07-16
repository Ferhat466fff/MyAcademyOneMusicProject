using OneMusic.BusinessLayer.Abstract;
using OneMusic.DataAccessLayer.Abstract;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BusinessLayer.ConCreate
{
    public class MessageManager : IMessageService
    {
        private readonly IMessageDal _messagedal;

        public MessageManager(IMessageDal messagedal)
        {
            _messagedal = messagedal;
        }

        public void TCreate(Message entity)
        {
            _messagedal.Create(entity);
        }

        public void TDelete(int id)
        {
            _messagedal.Delete(id);
        }

        public Message TGetById(int id)
        {
            return _messagedal.GetById(id);
        }

        public List<Message> TGetlist()
        {
            return _messagedal.Getlist();
        }

        public void TUpdate(Message entity)
        {
            _messagedal.Update(entity);
        }
    }
}
