from socket import inet_ntoa
import struct
from library.extentions.string_extentions import IPEndPoint
from servers.natneg.abstractions.contracts import CommonRequestBase, RequestBase
from servers.natneg.enums.general import (
    NatClientIndex,
    NatPortMappingScheme,
    NatPortType,
    NatType,
    PreInitState,
    RequestType,
)


class AddressCheckRequest(CommonRequestBase):
    pass


class ConnectAckRequest(RequestBase):
    client_index: NatClientIndex

    def parse(self) -> None:
        super().parse()
        self.client_index = NatClientIndex(self.raw_request[13])


class ConnectRequest(RequestBase):
    """
    Server will send this request to client to let them connect to each other
    """

    client_index: NatClientIndex


class ErtAckRequest(CommonRequestBase):
    pass


class InitRequest(CommonRequestBase):
    def __init__(self, raw_request: bytes) -> None:
        super().__init__(raw_request)
        self.game_name = None
        self.private_ip_endpoint = None

    def parse(self) -> None:
        super().parse()
        ip_bytes = self.raw_request[15:19]
        port_bytes = self.raw_request[19:21][::-1]
        port = struct.unpack("H", port_bytes)[0]
        ip_address_str = inet_ntoa(ip_bytes)
        self.private_ip_endpoint = IPEndPoint(ip_address_str, port)

        if len(self.raw_request) > 21 and self.raw_request[-1] == 0:
            game_name_bytes = self.raw_request[21:-1]
            self.game_name = game_name_bytes.decode("ascii")


class NatifyRequest(CommonRequestBase):
    pass


class PreInitRequest(RequestBase):
    state: PreInitState
    target_cookie: bytes

    def parse(self) -> None:
        super().parse()
        self.state = PreInitState(self.raw_request[12])
        self.target_cookie = int.from_bytes(self.raw_request[13:17], byteorder="big")


class ReportRequest(CommonRequestBase):
    is_nat_success: bool = None
    game_name: str
    nat_type: NatType
    mapping_scheme: NatPortMappingScheme

    def parse(self):
        super().parse()
        if len(self.raw_request) < 12:
            return
        self.version = self.raw_request[6]
        self.command_name = RequestType(self.raw_request[7])
        self.cookie = int.from_bytes(self.raw_request[8:12], byteorder="big")
        self.port_type = NatPortType(self.raw_request[12])
        self.client_index = NatClientIndex(self.raw_request[13])
        self.is_nat_success = False if self.raw_request[14] == 0 else True
        self.nat_type = NatType(self.raw_request[15])
        self.mapping_scheme = NatPortMappingScheme(self.raw_request[17])

        end_index = self.raw_request[23:].index(0)
        self.game_name = self.raw_request[23 : 23 + end_index].decode("ascii")
