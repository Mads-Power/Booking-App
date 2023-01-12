import { useRooms } from "@api/getRooms";
import { Box } from "@mui/system";
import {
  CircularProgress,
  Button,
  Container,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  SelectChangeEvent,
} from "@mui/material";
import { Link } from "react-router-dom";
import { Seat } from "@type/seat";
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

export const Rooms = () => {
  const { isLoading, data, error } = useRooms();
  const { selectedDate: date, setSelectedDate: setDate }: DateContextType =
    useDateContext(); //'2023-01-02T13:42:16.115Z'
  const [room, setRoom] = useState(1);
  const [selectedRoom, setSelectedRoom] = useState<Room>()
  const occupiedSeats = useBookingsByRoom(room, date);
  const loggedInUser = useUserContext().user;

  useEffect(() => {
    if (data) {
      if(!room) setRoom(1);
      setSelectedRoom(data[room - 1]);
    }
  }, [date, room, data]);

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
    if(occupied) {
      // If the seat is booked by the current logged in user, set the `fillHex` to be green, if not, set the color to be blue
      fillHex = occupiedSeats.data?.find(seat => seat.userId === loggedInUser.id) ? "#68B984" : "#3981F1"
    } else {
      // If seat is free, set `fillHex` to the color black
      fillHex = "#000000"
    }
    return fillHex
  }

  const handleDeskClick = (id: number) => {
  }
  const renderDeskSvgs = (seatId: number) => {
    return <svg xmlns="http://www.w3.org/2000/svg"
     height="48"
      width="48"
      onClick={() => handleDeskClick(seatId)}
      fill = {chooseDeskFill(seatId)}
      >
        <path d="M4 36V12h40v24h-3v-5h-9.5v5h-3V15H7v21Zm27.5-16H41v-5h-9.5Zm0 8H41v-5h-9.5Z" />
      </svg>
  }

  return (
    <div className="h-full overflow-hidden flex flex-col gap-4">
      <Container>
        <WeekViewDatePicker />
      </Container>
      <Container>
        <FormControl fullWidth>
          <InputLabel>Room</InputLabel>
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
      <Container>
      <div className="flex flex-row">
        <div className="grow">
              Opptatt
        </div>
        <div className="">
              Booket av deg
        </div>
        
      </div>
      </Container>

      {/* 1 = Storerommet / 2 = Lillerommet */}
    {room === 1 ? (
        <div className="w-80 mx-auto bg-slate-400 bg-opacity-10 flex flex-col p-4 gap-y-8">
          {selectedRoom ? (
            <div className="border border-solid border-black p-4 flex flex-col">
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
            <div>Kunne ikke hente valgt rom</div>
          )}
    <div>
            {selectedRoom ? (
              <div className="border border-solid border-black p-4 flex flex-col">
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
            ): (<></>)}
          </div>
    </div>) : (
    <div>dsa</div>
    )}
    </div>
  );
};
