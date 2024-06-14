from library.extentions.encoding import get_bytes
from servers.server_browser.v2.abstractions.contracts import (
    AdHocResponseBase,
    ServerListUpdateOptionResponseBase,
)
from servers.server_browser.v2.aggregations.server_info_builder import (
    build_server_info_header,
)
from servers.server_browser.v2.aggregations.string_flags import *
from servers.server_browser.v2.contracts.requests import ServerListRequest
from servers.server_browser.v2.contracts.results import (
    AdHocResult,
    P2PGroupRoomListResult,
    ServerMainListResult,
)

from servers.server_browser.v2.enums.general import GameServerFlags, ResponseType


class DeleteServerInfoResponse(AdHocResponseBase):
    _result: AdHocResult

    def __init__(self, result: AdHocResult) -> None:
        assert isinstance(result, AdHocResult)
        super().__init__(None, result)

    def build(self):
        buffer = bytearray()
        buffer.append(ResponseType.DELETE_SERVER_MESSAGE)
        buffer.extend(self._result.game_server_info.host_ip_address_bytes)
        self.sending_buffer = bytes(buffer)


class UpdateServerInfoResponse(AdHocResponseBase):
    def __init__(self, result: AdHocResult) -> None:
        assert isinstance(result, AdHocResult)
        super().__init__(None, result)

    def build(self) -> None:
        self._buffer.append(ResponseType.PUSH_SERVER_MESSAGE)
        self.__build_single_server_full_info()
        msg_leng = len(self._buffer).to_bytes(2, "big")
        self._buffer.insert(0, msg_leng)
        self.sending_buffer = bytes(self._buffer)

    def __build_single_server_full_info(self):
        header = build_server_info_header(
            GameServerFlags.HAS_FULL_RULES_FLAG, self._result.game_server_info
        )
        self._buffer.extend(header)
        if self._result.game_server_info.server_data is not None:
            server_data = UpdateServerInfoResponse._build_kv()
            self._buffer.extend(server_data)
        if self._result.game_server_info.player_data is not None:
            server_data = UpdateServerInfoResponse._build_kv()
            self._buffer.extend(server_data)
        if self._result.game_server_info.team_data is not None:
            server_data = UpdateServerInfoResponse._build_kv()
            self._buffer.extend(server_data)

    def _build_kv(data: dict):
        buffer = []
        for k, v in data.items():
            buffer.extend(get_bytes(k))
            buffer.append(STRING_SPLITER)
            buffer.extend(get_bytes(v))
            buffer.append(STRING_SPLITER)
        return buffer


class P2PGroupRoomListResponse(ServerListUpdateOptionResponseBase):
    _request: ServerListRequest
    _result: P2PGroupRoomListResult

    def build(self) -> None:
        super().build()
        self.build_server_keys()
        self.build_unique_value()
        self._build_servers_full_info()
        self.sending_buffer = bytes(self._servers_info_buffers)

    def _build_servers_full_info(self):
        for room in self._result.peer_room_infos:
            self._servers_info_buffers.append(GameServerFlags.HAS_KEYS_FLAG)
            group_id_bytes = room.group_id.to_bytes("big")
            self._servers_info_buffers.extend(group_id_bytes)
            for key in self._request.keys:
                self._servers_info_buffers.append(NTS_STRING_FLAG)
                value = (
                    room.raw_key_values[key]
                    if key in room.raw_key_values.keys()
                    else ""
                )
                self._servers_info_buffers.extend(get_bytes(value))
                self._servers_info_buffers.append(STRING_SPLITER)
        end_flag = int(0).to_bytes("little")
        self._servers_info_buffers.extend(end_flag)


class ServerMainListResponse(ServerListUpdateOptionResponseBase):
    _request: ServerListRequest
    _result: ServerMainListResult

    def __build_servers_full_info(self):
        for info in self._result.servers_info:
            header = build_server_info_header(self._result.flag, info)
            self._servers_info_buffers.extend(header)
            for key in self._request.keys:
                self._servers_info_buffers.append(NTS_STRING_FLAG)
                if key in info.server_data.keys():
                    self._servers_info_buffers.extend(get_bytes(info.server_data[key]))
                self._servers_info_buffers.append(STRING_SPLITER)

            self._servers_info_buffers.extend(ALL_SERVER_END_FLAG)

    def build(self) -> None:
        super().build()
        self.build_server_keys()
        self.build_unique_value()
        self.__build_servers_full_info()
        self.sending_buffer = bytes(self._servers_info_buffers)


class ServerNetworkInfoListResponse(ServerListUpdateOptionResponseBase):

    def build(self) -> None:
        super().build()
        self.sending_buffer = bytes(self._servers_info_buffers)
