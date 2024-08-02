from urllib.parse import urlparse
from library.src.abstractions.client import ClientBase
from library.src.abstractions.connections import ConnectionBase, ServerBase
from http.server import BaseHTTPRequestHandler, ThreadingHTTPServer
from library.src.unispy_server_config import CONFIG


class HttpRequest:
    url: str
    headers: dict
    content: str

    def __init__(self, url: str, headers: dict, content: str) -> None:
        self.url = url
        self.headers = headers
        self.content = content


class HttpResponse:
    def __init__(self, request: HttpRequest, content: str) -> None:
        assert isinstance(request, HttpRequest)
        assert isinstance(content, str)
        self.request = request
        self.content = content

    def get_content_bytes(self) -> bytes:
        return self.content.encode("ascii")


class HttpConnection(ConnectionBase):
    handler: BaseHTTPRequestHandler

    def send(self, data: bytes) -> None:
        self.handler.send_response(200)
        self.handler.send_header("Content-type", "text/xml")
        self.handler.end_headers()
        self.handler.wfile.write(data.get_content_bytes())


class HttpHandler(BaseHTTPRequestHandler):
    conn: HttpConnection

    def do_POST(self) -> None:
        parsed_url = urlparse(self.path)
        content_length = int(self.headers["Content-Length"])
        data = self.rfile.read(content_length).decode()
        request = HttpRequest(parsed_url, self.headers, data)
        if self.conn is None:
            self.conn = HttpConnection(self, *self.server.handler_params)
        self.conn.on_received(request)


class HttpServer(ServerBase):
    def start(self) -> None:
        self._server = ThreadingHTTPServer(
            (self._config.public_address, self._config.listening_port), HttpHandler
        )
        self._server.handler_params = (self._config, self._t_client, self._logger)
        self._server.serve_forever()


class TestClient(ClientBase):
    def create_switcher(self, buffer) -> None:
        # return super().create_switcher(buffer)
        print(buffer)
        pass

    def on_connected(self) -> None:
        # return super().on_connected()
        print("connected!")
        pass


if __name__ == "__main__":
    # create_http_server(list(CONFIG.servers.values())[0], ClientBase)
    s = HttpServer(list(CONFIG.servers.values())[0], TestClient, None)
