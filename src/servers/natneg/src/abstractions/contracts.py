import abc
import socket
from typing import Optional
import library.src.abstractions.contracts
from servers.natneg.src.enums.general import (
    NatClientIndex,
    NatPortType,
    RequestType,
    ResponseType,
)

MAGIC_DATA = bytes([0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2])


class RequestBase(library.src.abstractions.contracts.RequestBase):
    version: int
    cookie: int
    """
    byteorder:
        big
    """
    port_type: NatPortType
    command_name: RequestType
    raw_request: bytes

    def __init__(self, raw_request: Optional[bytes] = None):
        assert isinstance(raw_request, bytes)
        self.raw_request = raw_request

    def parse(self) -> None:
        if len(self.raw_request) < 12:
            return

        self.version = int(self.raw_request[6])
        self.command_name = RequestType(self.raw_request[7])
        self.cookie = int.from_bytes(self.raw_request[8:12])
        self.port_type = NatPortType(self.raw_request[12])


class ResultBase(library.src.abstractions.contracts.ResultBase):
    packet_type: ResponseType
    pass


class ResponseBase(library.src.abstractions.contracts.ResponseBase):
    _request: RequestBase
    _result: ResultBase
    sending_buffer: bytes

    def __init__(self, request: RequestBase, result: ResultBase) -> None:
        super().__init__(request, result)
        assert issubclass(type(request), RequestBase)
        assert issubclass(type(result), ResultBase)

    def build(self) -> None:
        data = bytes()
        data += MAGIC_DATA
        data += self._request.version.to_bytes(1, "little")
        data += self._result.packet_type.value.to_bytes(1, "little")
        data += self._request.cookie.to_bytes(4)
        self.sending_buffer = data


class CommonRequestBase(RequestBase):
    client_index: NatClientIndex
    use_game_port: bool

    def parse(self):
        super().parse()
        self.client_index = NatClientIndex(self.raw_request[13])
        self.use_game_port = bool(self.raw_request[14])


class CommonResultBase(ResultBase, abc.ABC):
    public_ip_addr: str
    public_port: int


class CommonResponseBase(ResponseBase):
    _result: CommonResultBase
    _request: CommonRequestBase

    def build(self) -> None:
        super().build()
        data = bytes()
        data += self.sending_buffer
        data += self._request.port_type.value.to_bytes(1, "little")
        data += self._request.client_index.value.to_bytes(1, "little")
        data += bytes(self._request.use_game_port)
        data += socket.inet_aton(self._result.public_ip_addr)
        data += self._result.public_port.to_bytes(2)
        self.sending_buffer = data
