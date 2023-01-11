<?php
// Function manager
if (isset($_POST['function'])) {
	switch ($_POST['function']) {
        case 'GetPositions':
            GetPositions($_POST['playerID']);
            break;
        case 'GetJumps':
            GetJumps($_POST['playerID']);
            break;
        case 'GetAttacks':
            GetAttacks($_POST['playerID']);
            break;
        case 'GetHits':
            GetHits($_POST['playerID']);
            break;
        case 'GetDeaths':
            GetDeaths($_POST['playerID']);
            break;
        case 'GetHeals':
            if (isset($_POST['playerID']))
                GetHeals($_POST['playerID']);
            else
                GetHeals(null);
            break;
        case 'GetCheckpoints':
            if (isset($_POST['playerID']))
                GetCheckpoints($_POST['playerID']);
            else
                GetCheckpoints(null);
            break;
        case 'GetInteractuables':
            GetInteractuables($_POST['playerID']);
            break;
		default:
			echo "No function defined.";
			break;
	}
}
else
{
    echo "Sin function no soy feliz:(";
}

// Get Positions
function GetPositions($playerID)
{
    include_once("config_db.php");

    if (is_null($playerID))
        $sql = "SELECT * FROM `positions`";
    else
        $sql = "SELECT * FROM `positions` WHERE `playerID` = '$playerID'";

    $result = $conn->query($sql);

    $lineSeparator = "|*|";
    $valueSeparator = "|/|";
    if ($result->num_rows > 0) {
        while($row = $result->fetch_assoc()) {
            echo $row["name"] . $valueSeparator . $row["posX"] . $valueSeparator . $row["posY"] . $valueSeparator . $row["posZ"] . $lineSeparator;
        }
    } else {
        echo "0 results";
    }
}

// Get Jumps
function GetJumps($playerID)
{
    include_once("config_db.php");

    if (is_null($playerID))
        $sql = "SELECT * FROM `jumps`";
    else
        $sql = "SELECT * FROM `jumps` WHERE `playerID` = '$playerID'";

    $result = $conn->query($sql);

    $lineSeparator = "|*|";
    $valueSeparator = "|/|";
    if ($result->num_rows > 0) {
        while($row = $result->fetch_assoc()) {
            echo $row["name"] . $valueSeparator . $row["posX"] . $valueSeparator . $row["posY"] . $valueSeparator . $row["posZ"] . $lineSeparator;
        }
    } else {
        echo "0 results";
    }
}

// Get Attacks
function GetAttacks($playerID)
{
    include_once("config_db.php");

    if (is_null($playerID))
        $sql = "SELECT * FROM `attacks`";
    else
        $sql = "SELECT * FROM `attacks` WHERE `playerID` = '$playerID'";

    $result = $conn->query($sql);

    $lineSeparator = "|*|";
    $valueSeparator = "|/|";
    if ($result->num_rows > 0) {
        while($row = $result->fetch_assoc()) {
            echo $row["name"] . $valueSeparator . $row["posX"] . $valueSeparator . $row["posY"] . $valueSeparator . $row["posZ"] . $lineSeparator;
        }
    } else {
        echo "0 results";
    }
}

// Get Hits
function GetHits($playerID)
{
    include_once("config_db.php");

    if (is_null($playerID))
        $sql = "SELECT * FROM `hits`";
    else
        $sql = "SELECT * FROM `hits` WHERE `playerID` = '$playerID'";

    $result = $conn->query($sql);

    $lineSeparator = "|*|";
    $valueSeparator = "|/|";
    if ($result->num_rows > 0) {
        while($row = $result->fetch_assoc()) {
            echo $row["name"] . $valueSeparator . $row["posX"] . $valueSeparator . $row["posY"] . $valueSeparator . $row["posZ"] . $lineSeparator;
        }
    } else {
        echo "0 results";
    }
}

// Get Deaths
function GetDeaths($playerID)
{
    include_once("config_db.php");

    if (is_null($playerID))
        $sql = "SELECT * FROM `deaths`";
    else
        $sql = "SELECT * FROM `deaths` WHERE `playerID` = '$playerID'";

    $result = $conn->query($sql);

    $lineSeparator = "|*|";
    $valueSeparator = "|/|";
    if ($result->num_rows > 0) {
        while($row = $result->fetch_assoc()) {
            echo $row["name"] . $valueSeparator . $row["posX"] . $valueSeparator . $row["posY"] . $valueSeparator . $row["posZ"] . $lineSeparator;
        }
    } else {
        echo "0 results";
    }
}

// Get Interactuables
function GetInteractuables ($playerID)
{
    include_once("config_db.php");

    if (is_null($playerID))
        $sql = "SELECT * FROM `interactuables`";
    else
        $sql = "SELECT * FROM `interactuables` WHERE `playerID` = '$playerID'";

    $result = $conn->query($sql);

    $lineSeparator = "|*|";
    $valueSeparator = "|/|";
    if ($result->num_rows > 0) {
        while($row = $result->fetch_assoc()) {
            echo $row["interactuableName"] . $valueSeparator . $row["posX"] . $valueSeparator . $row["posY"] . $valueSeparator . $row["posZ"] . $lineSeparator;
        }
    } else {
        echo "0 results";
    }
}

// Get Checkpoints
function GetCheckpoints ($playerID)
{
    include_once("config_db.php");

    if (is_null($playerID))
        $sql = "SELECT * FROM `checkpoints`";
    else
        $sql = "SELECT * FROM `checkpoints` WHERE `playerID` = '$playerID'";

    $result = $conn->query($sql);

    $lineSeparator = "|*|";
    $valueSeparator = "|/|";
    if ($result->num_rows > 0) {
        while($row = $result->fetch_assoc()) {
            echo $row["currentHealth"] . $valueSeparator . $row["posX"] . $valueSeparator . $row["posY"] . $valueSeparator . $row["posZ"] . $lineSeparator;
        }
    } else {
        echo "0 results";
    }
}

// Get Heals
function GetHeals ($playerID)
{
    include_once("config_db.php");

    if (is_null($playerID))
        $sql = "SELECT * FROM `heals`";
    else
        $sql = "SELECT * FROM `heals` WHERE `playerID` = '$playerID'";

    $result = $conn->query($sql);

    $lineSeparator = "|*|";
    $valueSeparator = "|/|";
    if ($result->num_rows > 0) {
        while($row = $result->fetch_assoc()) {
            echo $row["currentHealth"] . $valueSeparator . $row["posX"] . $valueSeparator . $row["posY"] . $valueSeparator . $row["posZ"] . $lineSeparator;
        }
    } else {
        echo "0 results";
    }
}


