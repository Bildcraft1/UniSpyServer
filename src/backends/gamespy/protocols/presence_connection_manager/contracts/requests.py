from dataclasses import dataclass
from typing import Union

import jsonschema
from pydantic import UUID4, BaseModel
from servers.presence_connection_manager.aggregates.user_status import UserStatus
from servers.presence_connection_manager.aggregates.user_status_info import (
    UserStatusInfo,
)
from servers.presence_connection_manager.enums.general import (
    LoginType,
    PublicMasks,
    SdkRevisionType,
)

import backends.gamespy.library.abstractions.request_base as lib


class RequestBase(BaseModel):
    server_id: UUID4


class ErrorOnParse(RequestBase):
    raw_request: str


# region buddy
class AddBuddyRequest(RequestBase):
    friend_profile_id: int
    reason: str


class DelBuddyRequest(RequestBase):
    friend_profile_id: int


class InviteToRequest(RequestBase):
    product_id: int
    profile_id: int
    session_key: str
    """the invite target profile id"""


class StatusInfoRequest(RequestBase):
    is_get_status_info: bool
    profile_id: int
    namespace_id: int
    status_info: UserStatusInfo


class StatusRequest(RequestBase):
    status: UserStatus
    is_get_status: bool


# region general


class KeepAliveRequest(RequestBase):
    pass


class LoginRequest(RequestBase):
    user_challenge: str
    response: str
    unique_nick: str
    user_data: str
    namespace_id: int
    auth_token: str
    nick: str
    email: str
    product_id: int
    type: Union[LoginType, int]
    sdk_revision_type: Union[SdkRevisionType, int]
    game_port: int
    user_id: int
    profile_id: int
    partner_id: int
    game_name: int
    quiet_mode_flags: int
    firewall: bool


class LogoutRequest(RequestBase):
    pass


# region profile


class AddBlockRequest(RequestBase):
    taget_id: int


class GetProfileRequest(RequestBase):
    profile_id: int
    session_key: str


class NewProfileRequest(RequestBase):
    is_replace_nick_name: bool
    session_key: str
    new_nick: str
    old_nick: str


class RegisterCDKeyRequest(RequestBase):
    session_key: str
    cdkey_enc: str


class RegisterNickRequest(RequestBase):
    unique_nick: str
    session_key: str
    partner_id: int


class UpdateProfileRequest(RequestBase):
    has_public_mask_flag: bool = None
    public_mask: Union[PublicMasks, int] = None
    session_key: str = None
    partner_id: int = None
    nick: str = None
    uniquenick: str = None
    has_first_name_flag: bool = None
    first_name: str = None
    has_last_name_flag: bool = None
    last_name: str = None
    has_icq_flag: bool = None
    icq_uin: int = None
    has_home_page_flag: bool = None
    home_page: str = None
    has_birthday_flag: bool = False
    birth_day: int = None
    birth_month: int = None
    birth_year: int = None
    has_sex_flag: bool = False
    sex: bool = None
    has_zip_code: bool = False
    zip_code: str = None
    has_country_code: bool = False
    country_code: str = None


class UpdateUiRequest(RequestBase):
    cpubrandid: str = None
    cpuspeed: str = None
    memory: str = None
    videocard1ram: str = None
    videocard2ram: str = None
    connectionid: str = None
    connectionspeed: str = None
    hasnetwork: str = None
    pic: str = None


if __name__ == "__main__":
    from jsonschema import validate

    data = {
        "server_id": "550e8400-e29b-41d4-a716-446655440000",
        "raw_request": "hello",
        "friend_profile_id": 1,
        "reason": "hello",
    }
    r = AddBuddyRequest(**data)
    r.validate()
    pass
