using UniSpy.Server.GameStatus.Abstraction.BaseClass;
using UniSpy.Server.GameStatus.Exception;
using UniSpy.Server.GameStatus.Contract.Request;
using UniSpy.Server.Core.Abstraction.Interface;

namespace UniSpy.Server.GameStatus.Handler.CmdHandler
{
    /// <summary>
    /// Handles game snapshot and update game data
    /// </summary>

    public sealed class UpdateGameHandler : CmdHandlerBase
    {
        //old request "\updgame\\sesskey\%d\done\%d\gamedata\%s"
        //new request "\updgame\\sesskey\%d\connid\%d\done\%d\gamedata\%s"
        private new UpdateGameRequest _request => (UpdateGameRequest)base._request;
        public UpdateGameHandler(IClient client, IRequest request) : base(client, request)
        {
        }
        protected override void RequestCheck()
        {
            base.RequestCheck();
            if (_request.SessionKey != _client.Info.GameSessionKey)
            {
                throw new GSException("Game session key is not match");
            }
        }
        protected override void DataOperation()
        {
            if (_request.GameData is null)
            {
                // the gamedata is null, we do not need to process this request
                return;
            }
            // replace game data with new data
            throw new GSException("Implement update game handler.");
        }
    }
}
