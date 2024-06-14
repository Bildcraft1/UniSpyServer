from servers.chat.abstractions.contract import SERVER_DOMAIN
from servers.chat.enums.irc_error_code import IRCErrorCode
import abc

from library.exceptions.error import UniSpyException as ER


class ChatException(ER):
    pass


class IRCException(ChatException):
    error_code: "IRCErrorCode"
    sending_buffer: "str"

    def __init__(self, message: "str", error_code: "IRCErrorCode") -> None:
        super().__init__(message)
        assert isinstance(error_code, IRCErrorCode)
        self.error_code = error_code

    @abc.abstractedmethod
    def build(self):
        pass


class IRCChannelException(IRCException):
    channel_name: str

    def __init__(self, message: str, error_code: IRCErrorCode) -> None:
        super().__init__(message, error_code)

    def build(self):
        self.sending_buffer = f":{SERVER_DOMAIN} {self.error_code} * {self.channel_name} :{self.message}\r\n"


class ErrOneUSNickNameException(IRCException):
    def __init__(
        self,
        message: "str",
        error_code: "IRCErrorCode" = IRCErrorCode.ERR_ONE_US_NICK_NAME,
    ) -> None:
        super().__init__(message, error_code)


class LoginFailedException(IRCException):
    def __init__(
        self,
        message: "str",
        error_code: "IRCErrorCode" = IRCErrorCode.LOGIN_FAILED,
    ) -> None:
        super().__init__(message, error_code)


class MoreParametersException(IRCException):
    def __init__(
        self,
        message: "str",
        error_code: "IRCErrorCode" = IRCErrorCode.MORE_PARAMETERS,
    ) -> None:
        super().__init__(message, error_code)


class NickNameInUseException(IRCException):
    old_nick: str
    new_nick: str

    def __init__(
        self,
        old_nick: str,
        new_nick: str,
        message: str,
        error_code: IRCErrorCode = IRCErrorCode.NICK_NAME_IN_USE,
    ) -> None:
        super().__init__(message, error_code)
        self.old_nick = old_nick
        self.new_nick = new_nick

    def build(self):
        self.sending_buffer = (
            f"{SERVER_DOMAIN} {self.error_code} {self.old_nick} {self.new_nick} *\r\n"
        )


class NoSuchNickException(IRCException):
    def __init__(
        self, message: str, error_code: IRCErrorCode = IRCErrorCode.NO_SUCH_NICK
    ) -> None:
        super().__init__(message, error_code)


class NoUniqueNickException(IRCException):
    def __init__(
        self, message: str, error_code: IRCErrorCode = IRCErrorCode.NO_UNIQUE_NICK
    ) -> None:
        super().__init__(message, error_code)


class RegisterNickFaildException(IRCException):
    def __init__(
        self, message: str, error_code: IRCErrorCode = IRCErrorCode.REGISTER_NICK_FAILED
    ) -> None:
        super().__init__(message, error_code)


class TooManyChannelsException(IRCException):
    def __init__(
        self, message: str, error_code: IRCErrorCode.TOO_MANY_CHANNELS
    ) -> None:
        super().__init__(message, error_code)


class UniqueNickExpiredException(IRCException):
    def __init__(
        self, message: str, error_code: IRCErrorCode = IRCErrorCode.UNIQUE_NICK_EXPIRED
    ) -> None:
        super().__init__(message, error_code)
