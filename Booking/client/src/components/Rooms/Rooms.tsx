import { useRooms } from "@api/getRooms";
import { Box } from "@mui/system";
import {
  CircularProgress,
  Container,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  SelectChangeEvent,
} from "@mui/material";
import { WeekViewDatePicker } from "@components/WeekViewDatePicker";
import "./Rooms.module.css";
import { useEffect, useState } from "react";
import { useBookingsByRoom } from "@api/getBookingsByRoom";
import {
  DateContextType,
  useDateContext,
} from "@components/Provider/DateContextProvider";
import { useUserContext } from "@components/Provider/UserContextProvider";
import { Room } from "@type/room";
import { useNavigate } from "react-router-dom";

export const Rooms = () => {
  const { isLoading, data, error } = useRooms();
  const { selectedDate: date, setSelectedDate: setDate }: DateContextType =
    useDateContext(); //'2023-01-02T13:42:16.115Z'
  const [room, setRoom] = useState(1);
  const [selectedRoom, setSelectedRoom] = useState<Room>()
  const occupiedSeats = useBookingsByRoom(room, date);
  const loggedInUser = useUserContext().user;
  const navigate = useNavigate();

  useEffect(() => {
    if (data) {
      if (!room) setRoom(1);
      setSelectedRoom(data[room - 1]);
    }
    console.log(occupiedSeats.data);
    
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

  const chooseDeskFill = (seatId: number) => {
    let fillHex = "";
    const occupied = occupiedSeats.data?.find(seat => seat.seatId === seatId);
    if (occupied) {
      // If the seat is booked by the current logged in user, set the `fillHex` to be green, if not, set the color to be blue
      occupiedSeats.data?.some(seat => {
        if(seat.userId === loggedInUser.id && seat.seatId === seatId) {
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
    navigate(`/seat/${id}`, { relative: "path"})
  }

  const renderDeskSvgs = (seatId: number) => {
    return <svg xmlns="http://www.w3.org/2000/svg"
      height="48"
      width="48"
      onClick={() => handleDeskClick(seatId)}
      fill={chooseDeskFill(seatId)}
    >
      <path d="M4 36V12h40v24h-3v-5h-9.5v5h-3V15H7v21Zm27.5-16H41v-5h-9.5Zm0 8H41v-5h-9.5Z" />
    </svg>
  }

  return (
    <div className="h-full overflow-hidden flex flex-col gap-4">
      <Container>
        <WeekViewDatePicker />
      </Container>
      <Container className="mt-12 mb-8">
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
      </Container>
      <Container className="mb-4">
      <div className="flex flex-row">
        <Box className="grow flex flex-row border w-1/2 h-8 mr-1">
          <div className="py-1">
            <svg xmlns="http://www.w3.org/2000/svg" className="fill-[#3981F1]" height="24" width="24"><path d="M6 18V6h12v12Z"/></svg>
          </div>
          <Container className="text-center">
            <span className="align-middle text-sm">
              Opptatt
            </span>
          </Container>
        </Box>
        <Box className="grow flex flex-row border w-1/2 h-8 ml-1 ">
          <div className="py-1">
            <svg xmlns="http://www.w3.org/2000/svg" className="fill-[#68B984]" height="24" width="24"><path d="M6 18V6h12v12Z"/></svg>
          </div>
          <Container className="text-center">
            <span className="align-middle text-sm">
              Booket av deg
            </span>
          </Container>
        </Box>    
      </div>
      </Container>

      {/* 1 = Storerommet / 2 = Lillerommet */}
      {room === 1 ? (
        <div className="w-80 mx-auto bg-slate-400 bg-opacity-10 flex flex-col p-4 gap-y-8">
          {selectedRoom ? (
            <div className="p-4">
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
        <div className="w-80 mx-auto bg-slate-400 bg-opacity-10">
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
  );
};
