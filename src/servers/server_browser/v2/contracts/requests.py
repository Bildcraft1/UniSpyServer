from socket import inet_ntoa
from servers.server_browser.v2.abstractions.contracts import (
    AdHocRequestBase,
    ServerListUpdateOptionRequestBase,
)
from servers.server_browser.v2.enums.general import RequestType, ServerListUpdateOption


class ServerListRequest(ServerListUpdateOptionRequestBase):
    command_name: RequestType = RequestType.SERVER_LIST_REQUEST

    def parse(self) -> None:
        self.request_version = int(self.raw_request[2])
        self.protocol_version = int(self.raw_request[3])
        self.encoding_version = int(self.raw_request[4])
        self.game_version = int(self.raw_request[5:9])

        remain_data = self.raw_request[9:]
        dev_game_name_index = remain_data.index(0)
        self.dev_game_name = remain_data[:dev_game_name_index].decode()
        remain_data = remain_data[dev_game_name_index + 1 :]
        game_name_index = remain_data.index(0)
        self.game_name = remain_data[:game_name_index].decode()
        remain_data = remain_data[game_name_index + 1 :]
        self.client_challenge = remain_data[:8].decode()
        remain_data = remain_data[8:]

        filter_index = remain_data.index(0)
        if filter_index > 0:
            self.filter = remain_data[:filter_index].decode()
        remain_data = remain_data[filter_index + 1 :]

        keys_index = remain_data.index(0)
        self.keys = remain_data[:keys_index].decode().split("\\")
        remain_data = remain_data[keys_index + 1 :]

        byte_update_options = remain_data[:4][::-1]
        self.update_option = ServerListUpdateOption(int(byte_update_options))
        remain_data = remain_data[4:]

        if self.update_option & ServerListUpdateOption.ALTERNATE_SOURCE_IP:
            self.source_ip = inet_ntoa(remain_data[:4])
            remain_data = remain_data[4:]

        if self.update_option & ServerListUpdateOption.LIMIT_RESULT_COUNT:
            if len(remain_data) != 4:
                raise Exception("The max number of server is incorrect.")
            self.max_servers = int(remain_data[:4][::-1])


class SendMessageRequest(AdHocRequestBase):
    prefix_message: bytes
    client_message: bytes

    def parse(self) -> None:
        super().parse()
        self.client_message = self.raw_request[9:]


class ServerInfoRequest(AdHocRequestBase):
    pass
