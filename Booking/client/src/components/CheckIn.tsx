// at first creates a counter

import { useState } from "react";

const CheckIn = () => {
    const [checkedIn, setCheckedIn] = useState(0)
    return <div>
        <button>sjekk inn</button>
    </div>
} 

export default CheckIn;