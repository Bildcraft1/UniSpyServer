from typing import Any, Literal, Optional
from uuid import UUID
import os

from pydantic import BaseModel, Field, constr

from library.src.exceptions.error import UniSpyException


class PostgreSql(BaseModel):
    server: str
    port: int = Field(..., ge=1, le=65535)  # Ensures port is between 1 and 65535
    database: str
    username: str
    password: str
    ssl_mode: str  # You might want to restrict this to specific values
    trust_server_cert: bool
    ssl_key: Optional[str] = None  # Optional field for SSL key
    ssl_password: Optional[str] = None  # Optional field for SSL password
    root_cert: Optional[str] = None  # Optional field for root certificate
    url: str = None  # URL will be generated based on other fields

    def model_post_init(self, __context: Any) -> None:
        self.url = f"postgresql://{self.username}:{self.password}@{self.server}:{self.port}/{self.database}?sslmode={self.ssl_mode}"


class RedisConfig(BaseModel):
    server: str
    port: int = Field(..., ge=1, le=65535)  # Ensures port is between 1 and 65535
    user: str
    password: str
    ssl: bool  # Use bool for SSL flag
    ssl_host: Optional[str] = None  # Optional field for SSL host
    url: str = None  # URL will be generated based on other fields

    def model_post_init(self, __context: Any) -> None:
        if self.ssl:
            self.url = (
                f"rediss://{self.user}:{self.password}@{self.server}:{self.port}/0"
            )
        else:
            self.url = (
                f"redis://{self.user}:{self.password}@{self.server}:{self.port}/0"
            )


class ServerConfig(BaseModel):
    server_id: UUID
    server_name: str = constr(min_length=1)  # Ensures server_name is a non-empty string
    public_address: str = constr(
        min_length=1
    )  # Ensures public_address is a non-empty string
    listening_port: int = Field(
        ..., ge=1, le=65535
    )  # Ensures listening_port is between 1 and 65535


class LoggingConfig(BaseModel):
    path: str
    min_log_level: Literal["debug", "info", "warning", "error"]


class BackendConfig(BaseModel):
    url: str


class MongoDbConfig(BaseModel):
    server: str
    username: str
    password: str
    port: int = Field(default=None)
    database: str | None = Field(default=None)
    url: str = Field(default=None)

    def model_post_init(self, __context: Any) -> None:
        url = f"mongodb+srv://{self.username}:{self.password}@{self.server}"
        if self.port is not None:
            url += f":{self.port}"
        if self.database is not None:
            url += f"/{self.database}"
        return url


class UniSpyServerConfig(BaseModel):
    postgresql: PostgreSql
    redis: RedisConfig
    backend: BackendConfig
    servers: dict[str, ServerConfig] = Field(default_factory=dict)
    mongodb: MongoDbConfig



unispy_config = os.environ.get("UNISPY_CONFIG")
if unispy_config is None:
    raise UniSpyException(
        "Unispy server config not found, you should set the UNISPY_CONFIG in the system enviroment."
    )
with open(unispy_config, "r") as f:
    import json

    config = json.load(f)
    CONFIG = UniSpyServerConfig(**config)
    pass

if __name__ == "__main__":
    pass
