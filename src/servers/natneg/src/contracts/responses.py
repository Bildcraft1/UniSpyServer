from servers.natneg.src.abstractions.contracts import CommonResponseBase
from servers.natneg.src.contracts.requests import (
    AddressCheckRequest,
    ErtAckRequest,
    InitRequest,
    NatifyRequest,
)
from servers.natneg.src.contracts.results import AddressCheckResult, ErtAckResult, InitResult, NatifyResult


class InitResponse(CommonResponseBase):
    _request: InitRequest
    _result: InitResult

    def __init__(self, request: "InitRequest", result: "InitResult") -> None:
        super().__init__(request, result)
        assert isinstance(request, InitRequest)
        assert isinstance(result, InitResult)


class ErcAckResponse(InitResponse):
    _request: ErtAckRequest
    _result: ErtAckResult

    def __init__(self, request: "ErtAckRequest", result: "ErtAckResult") -> None:
        assert isinstance(request, InitRequest)
        assert isinstance(result, InitResult)
        self._request = request
        self._result = result


class NatifyResponse(InitResponse):
    _request: NatifyRequest
    _result: NatifyResult

    def __init__(self, request: "NatifyRequest", result: "NatifyResult") -> None:
        assert isinstance(request, NatifyRequest)
        assert isinstance(result, NatifyResult)
        self._request = request
        self._result = result


class AddressCheckResponse(InitResponse):
    _request: "AddressCheckRequest"
    _result: "AddressCheckResult"

    def __init__(self, request: "AddressCheckRequest", result: "AddressCheckResult") -> None:
        assert isinstance(request, AddressCheckRequest)
        assert isinstance(result, AddressCheckResult)
        self._request = request
        self._result = result
