import unittest
from library.extentions.bytes_extentions import int_to_bytes
from servers.natneg.contracts.requests import (
    AddressCheckRequest,
    ErtAckRequest,
    InitRequest,
    NatifyRequest,
    PreInitRequest,
)
from servers.natneg.enums.general import (
    NatClientIndex,
    NatPortType,
    PreInitState,
    RequestType,
)


class UnitTests(unittest.TestCase):
    def test_init(self) -> None:
        raw = bytes(
            [
                0xfd, 0xfc, 0x1e, 0x66, 0x6a, 0xb2, 0x03,
                0x00,
                0x00, 0x00, 0x03, 0x09, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            ]
        )  # fmt: skip
        req = InitRequest(raw)
        req.parse()
        cookie = 151191552
        self.assertEqual(cookie.to_bytes(4, "little"), req.cookie)
        self.assertEqual(RequestType.INIT, req.command_name)
        self.assertEqual(NatClientIndex.GAME_CLIENT, req.client_index)
        self.assertEqual(False, req.use_game_port)
        self.assertEqual(3, req.version)
        self.assertEqual(NatPortType.NN1, req.port_type)

    def test_address_check(self) -> None:
        raw = bytes(
            [
                0xfd, 0xfc, 0x1e, 0x66, 0x6a, 0xb2, 0x03, 0x0a, 0x00, 0x00, 0x03, 0x09, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            ]
        )  # fmt: skip

        req = AddressCheckRequest(raw)
        req.parse()
        cookie = 151191552
        self.assertEqual(cookie.to_bytes(4, "little"), req.cookie)
        self.assertEqual(RequestType.ADDRESS_CHECK, req.command_name)
        self.assertEqual(NatClientIndex.GAME_CLIENT, req.client_index)
        self.assertEqual(False, req.use_game_port)
        self.assertEqual(3, req.version)
        self.assertEqual(NatPortType.NN1, req.port_type)

    def test_ert_ack(self) -> None:
        raw = bytes(
            [
                0xfd, 0xfc, 0x1e, 0x66, 0x6a, 0xb2, 0x03,
                0x03,
                0x00, 0x00, 0x03, 0x09,
                0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            ]
        )  # fmt: skip
        req = ErtAckRequest(raw)
        req.parse()
        cookie = 151191552
        self.assertEqual(cookie.to_bytes(4, "little"), req.cookie)
        self.assertEqual(RequestType.ERT_ACK, req.command_name)
        self.assertEqual(NatClientIndex.GAME_CLIENT, req.client_index)
        self.assertEqual(3, req.version)
        self.assertEqual(False, req.use_game_port)
        self.assertEqual(NatPortType.NN1, req.port_type)

    def test_nattify(self) -> None:
        raw = bytes(
            [
                0xfd, 0xfc, 0x1e, 0x66, 0x6a, 0xb2, 0x03,
                0x0c,
                0x00, 0x00, 0x03, 0x09,
                0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            ]
        )  # fmt: skip
        req = NatifyRequest(raw)
        req.parse()
        cookie = 151191552
        self.assertEqual(cookie.to_bytes(4, "little"), req.cookie)
        self.assertEqual(RequestType.NATIFY_REQUEST, req.command_name)
        self.assertEqual(NatClientIndex.GAME_CLIENT, req.client_index)
        self.assertEqual(3, req.version)
        self.assertEqual(False, req.use_game_port)
        self.assertEqual(NatPortType.NN1, req.port_type)

    def test_preinit(self) -> None:

        raw = bytes(
            [
                0xfd, 0xfc, 0x1e, 0x66, 0x6a, 0xb2, 0x04, 0x0f, 0xb5, 0xe0, 0x95, 0x2a, 0x00, 0x24, 0x38, 0xb2, 0xb3, 0x5e
            ]
        )  # fmt: skip

        req = PreInitRequest(raw)
        req.parse()
        b_cookie = bytes(
            [
                0xB5,
                0xE0,
                0x95,
                0x2A,
            ]
        )
        self.assertEqual(b_cookie, req.cookie)
        self.assertEqual(RequestType.PRE_INIT, req.command_name)
        self.assertEqual(4, req.version)
        self.assertEqual(NatPortType.GP, req.port_type)
        self.assertEqual(PreInitState.WAITING_FOR_CLIENT, req.state)


if __name__ == "__main__":
    unittest.main()
