import { useRoomsQuery } from '@api/useRoomsQuery';
import { Box } from '@mui/system';
import { CircularProgress, Button, Container, SelectChangeEvent, FormControl, InputLabel, Select, MenuItem } from '@mui/material';
import { WeekViewDatePicker } from '@components/WeekViewDatePicker';
import './Rooms.module.css';
import { useEffect, useState } from 'react';
import { useBookingsByRoomQuery } from '@api/useBookingsByRoomQuery';
import { useAtom } from 'jotai';
import { dateAtom } from '../../jotaiProvider';
import { useUserQuery } from '@api/useUserQuery';
import { Room } from "@type/room";
import { useNavigate } from 'react-router-dom';

export const Rooms = () => {
  const { isLoading, data, error } = useRoomsQuery();
  const [date, setDate] = useAtom(dateAtom);
  const occupiedSeats = useBookingsByRoomQuery(1, date);
  const { data: loggedInUser } = useUserQuery('5');
  const [room, setRoom] = useState(1);
  const [selectedRoom, setSelectedRoom] = useState<Room>()
  const navigate = useNavigate();

  useEffect(() => {
    if (data) {
      if (!room) setRoom(1);
      setSelectedRoom(data[room - 1]);
    }
  }, [date, room, data, occupiedSeats]);

  if (isLoading) {
    return <CircularProgress size={100} />;
  }

  if (error) {
    return <h4>No Rooms Found</h4>;
  }

  const handleChangeRoom = (e: SelectChangeEvent) => {
    const val = parseInt(e.target.value);
    if (data) {
      setRoom(val);
      setSelectedRoom(data[val - 1]);
    }
  };

  const handleSelectRoom = (roomId: number) => {
    if (data) {
      setRoom(roomId);
      setSelectedRoom(data[roomId - 1]);
    }
  }

  const chooseDeskFill = (seatId: number) => {
    let fillHex = "";
    const occupied = occupiedSeats.data?.find(seat => seat.seatId === seatId);
    if (occupied) {
      // If the seat is booked by the current logged in user, set the `fillHex` to be green, if not, set the color to be blue
      occupiedSeats.data?.some(seat => {
        if (seat.userId === loggedInUser?.id && seat.seatId === seatId) {
          fillHex = "#68B984";
          return true;
        }
        else {
          fillHex = "#3981F1";
        }
      })
    } else {
      // If seat is free, set `fillHex` to the color black
      fillHex = "#000000"
    }
    return fillHex
  }

  const handleDeskClick = (id: number) => {
    navigate(`/seat/${id}`, { relative: "path" })
  }

  const renderDeskSvgs = (seatId: number) => {
    return <svg xmlns="http://www.w3.org/2000/svg"
      height="48"
      width="48"
      onClick={() => handleDeskClick(seatId)}
      fill={chooseDeskFill(seatId)}
      className="transition ease-in-out delay-75 hover:scale-125 lg:h-16 lg:w-16"
    >
      <path d="M4 36V12h40v24h-3v-5h-9.5v5h-3V15H7v21Zm27.5-16H41v-5h-9.5Zm0 8H41v-5h-9.5Z" />
    </svg>
  }

  const renderMenuButtonForLargeScreens = (room: Room) => {
    return <div className={"w-full bg-opacity-20 flex flex-row shadow-md mx-auto justify-between " + (selectedRoom?.id === room.id ? 'bg-slate-800' : 'bg-slate-400')}>
      <div className="p-2 m-2">
        <p className="text-base">{room.name}</p>
      </div>
      <div
        className="basis-1/4 h-full flex align-middle bg-slate-400 bg-opacity-10">
        <svg xmlns="http://www.w3.org/2000/svg"
          height="20"
          width="20"
          className="mx-auto self-center"
        ><path d="M9.4 18 8 16.6l4.6-4.6L8 7.4 9.4 6l6 6Z" /></svg>
      </div>
    </div>
  }

  return (
    <div className="h-full w-[85%] overflow-hidden flex flex-col gap-4 my-10 mx-auto">
      <Container>
        <WeekViewDatePicker />
      </Container>
      {/* This div will be hidden on screens smaller than 1024 px */}
      <div className="w-full mt-12 mb-8 lg:hidden">
        <FormControl fullWidth>
          <InputLabel>Rom</InputLabel>
          <Select
            value={room.toString()}
            label="Room"
            onChange={handleChangeRoom}
          >
            {data?.map((rooms) => (
              <MenuItem key={rooms.id} value={rooms.id}>{rooms.name}</MenuItem>
            ))}
          </Select>
        </FormControl>
      </div>
      <div className="w-[90%] lg:w-[70%] mx-auto">
        <div className="flex flex-row w-full">
          <Box className="grow flex flex-row border w-1/2 h-8 mr-1">
            <div className="py-1">
              <svg xmlns="http://www.w3.org/2000/svg" className="fill-[#3981F1]" height="24" width="24"><path d="M6 18V6h12v12Z" /></svg>
            </div>
            <Container className="text-center">
              <span className="align-middle text-sm truncate">
                Opptatt
              </span>
            </Container>
          </Box>
          <Box className="grow flex flex-row border w-1/2 h-8 ml-1 ">
            <div className="py-1">
              <svg xmlns="http://www.w3.org/2000/svg" className="fill-[#68B984]" height="24" width="24"><path d="M6 18V6h12v12Z" /></svg>
            </div>
            <Container className="text-center">
              <span className="align-middle text-sm truncate">
                Booket av deg
              </span>
            </Container>
          </Box>
        </div>
      </div>

      {/* 1 = Storerommet / 2 = Lillerommet */}
      <div className="w-[90%] lg:w-[80%] lg:flex lg:flex-row lg:justify-around lg:child:mx-2 grow child:h-[80%] mx-auto ">
        {/* This div will be hidden on all screens smaller than 1024px */}
        <div className="hidden lg:block basis-1/4 bg-slate-400 bg-opacity-10 p-2 overflow-hidden">
          <div className="w-full flex flex-col h-full gap-y-4">
            {data?.map((room) => (
              <div 
              key={room.id} 
              className="w-full flex hover:cursor-pointer"
              onClick={() => handleSelectRoom(room.id)}>
                {renderMenuButtonForLargeScreens(room)}
              </div>
            ))}
          </div>
        </div>
        <div className="lg:basis-3/4 lg:w-[90%] bg-slate-400 bg-opacity-10 ">
          {room === 1 ? (
            <div className="w-full mx-auto child:p-2">
              {selectedRoom ? (
                <div>
                  <div className="flex flex-col">
                    <div className="flex flex-row-reverse gap-x-6">
                      {renderDeskSvgs(1)}
                      {renderDeskSvgs(2)}
                      {renderDeskSvgs(3)}
                    </div>
                    <div className="flex flex-row-reverse gap-x-6">
                      {renderDeskSvgs(4)}
                      {renderDeskSvgs(5)}
                      {renderDeskSvgs(6)}
                    </div>
                  </div>
                </div>
              ) : (
                <div>Kunne ikke hente rommet som ble valgt</div>
              )}
              <div>
                {selectedRoom ? (
                  <div className="p-4 flex flex-col">
                    <div className="flex flex-col">
                      <div className="flex flex-row-reverse gap-x-6">
                        {renderDeskSvgs(7)}
                        {renderDeskSvgs(8)}
                      </div>
                      <div className="flex flex-row-reverse gap-x-6">
                        {renderDeskSvgs(9)}
                        {renderDeskSvgs(10)}
                      </div>
                    </div>
                  </div>
                ) : (<></>)}
              </div>
            </div>) : (
            <div className="w-full lg:w-auto mx-auto bg-slate-400 bg-opacity-10">
              {selectedRoom ? (
                <div className="p-4 flex flex-col gap-y-4">
                  <div className="flex flex-row-reverse gap-x-6 justify-center">
                    {renderDeskSvgs(11)}
                    {renderDeskSvgs(12)}
                  </div>
                  <div className="flex flex-row justify-end">
                    {renderDeskSvgs(13)}
                  </div>
                  <div className="flex flex-row justify-between">
                    {renderDeskSvgs(14)}
                    {renderDeskSvgs(15)}
                  </div>
                </div>
              ) : (
                <div>Kunne ikke hente rommet som ble valgt</div>
              )}
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
