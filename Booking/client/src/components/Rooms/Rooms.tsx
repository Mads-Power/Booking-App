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
  const [ selectedRoom, setSelectedRoom ] = useState<Room>()
  const occupiedSeats = useBookingsByRoom(room, date);
  const loggedInUser = useUserContext().user;

  useEffect(() => {}, [date, room]);

  if (isLoading) {
    return <CircularProgress size={100} />;
  }

  if (error) {
    return <h4>No Rooms Found</h4>;
  }

  const handleChangeRoom = (e: SelectChangeEvent) => {
    const val = parseInt(e.target.value);
    if(data) {
      setRoom(val);
      setSelectedRoom(data[val - 1])
    }
  };

  const renderIsOccupied = (seatId: number) => {
    const predicate =
      occupiedSeats.data?.find((o) => o.seatId == seatId) != undefined;
    return predicate;
  };

  const renderOccupiedByLoggedInUser = (seatId: number) => {
    const predicate =
      occupiedSeats.data?.find(
        (o) => o.seatId == seatId && o.userId == loggedInUser.id
      ) != undefined;
    return predicate;
  };

  return (
    <>
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

    {room === 1 ? (
    <div>
      <h1>{selectedRoom?.name}</h1>
    </div>) : (
    <div>dsa</div>
    )}
    </>
  );
};
