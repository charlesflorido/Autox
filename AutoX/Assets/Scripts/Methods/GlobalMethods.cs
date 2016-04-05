using UnityEngine;
using System.Collections;

public class GlobalMethods {

    public static bool TestMethod(MethodCall methodCall)
    {
        bool ret = true;

        if (methodCall.name == "add")
        {
            if (methodCall.parameters.Length != 2)
            {
                methodCall.printError();
                ret = false;
            }
        }
        else if(methodCall.name == "printint")
        {
            if (methodCall.parameters.Length != 1) {
                methodCall.printError();
                ret = false;
            }
        }
        else if (methodCall.name == "setengine")
        {
            if (methodCall.parameters.Length != 1)
            {
                methodCall.printError();
                ret = false;
            }
        }
        else if (methodCall.name == "isengineon")
        {
            if (methodCall.parameters.Length != 0)
            {
                methodCall.printError();
                ret = false;
            }
        }
        else if (methodCall.name == "askdriver")
        {
            if(methodCall.parameters.Length == 0)
            {

            }
            else if(methodCall.parameters.Length == 3)
            {

            }
            else
            {
                methodCall.printError();
                ret = false;
            }
        }
        else if (methodCall.name == "isotherroadexist")
        {
            if (methodCall.parameters.Length != 0)
            {
                methodCall.printError();
                ret = false;
            }
        }
        else if (methodCall.name == "stepgas")
        {
            if (methodCall.parameters.Length != 1)
            {
                methodCall.printError();
                ret = false;
            }
        }
        else if (methodCall.name == "stepclutch")
        {
            if (methodCall.parameters.Length != 1)
            {
                methodCall.printError();
                ret = false;
            }
        }
        else if (methodCall.name == "changegear")
        {
            if (methodCall.parameters.Length != 1)
            {
                methodCall.printError();
                ret = false;
            }
        }
        else if (methodCall.name == "wait")
        {
            if (methodCall.parameters.Length != 1)
            {
                methodCall.printError();
                ret = false;
            }
        }
        else if (methodCall.name == "getgear")
        {
            if (methodCall.parameters.Length != 0)
            {
                methodCall.printError();
                ret = false;
            }
        }
        else if (methodCall.name == "isrightroadavailable")
        {
            if (methodCall.parameters.Length != 0)
            {
                methodCall.printError();
                ret = false;
            }
        }
        else if (methodCall.name == "isleftroadavailable")
        {
            if (methodCall.parameters.Length != 0)
            {
                methodCall.printError();
                ret = false;
            }
        }
        else if (methodCall.name == "isforwardroadavailable")
        {
            if (methodCall.parameters.Length != 0)
            {
                methodCall.printError();
                ret = false;
            }
        }
        else
        {
            methodCall.printError();
            ret = false;
        }

        return ret;
    }

    public static void ExecuteMethod(MethodCall methodCall)
    {
        if (methodCall.name == "add")
        {
            if (methodCall.parameters.Length != 2)
                methodCall.printError();
            else
            {
                methodCall.returnValue = Add(methodCall.parameters[0], methodCall.parameters[1]);
            }
        }
        else if (methodCall.name == "printint")
        {
            if (methodCall.parameters.Length != 1)
                methodCall.printError();
            else
            {
                printInt(methodCall.parameters[0]);
            }
        }
        else if (methodCall.name == "setengine")
        {
            if (methodCall.parameters.Length != 1)
                methodCall.printError();
            else
            {
                int num = methodCall.parameters[0];
                bool param = (num == 0)?false: true;
                CarController.instance.setEngine(param);
            }
        }
        else if (methodCall.name == "isengineon")
        {
            if (methodCall.parameters.Length != 0)
                methodCall.printError();
            else
            {
                bool ret = CarController.instance.isEngineOn();
                methodCall.returnValue = (ret) ? 1 : 0;     
            }
        }
        else if (methodCall.name == "askdriver")
        {
            if (methodCall.parameters.Length == 0)
            {
                CarController.instance.askDriver();
            }
            else if(methodCall.parameters.Length == 3)
            {
                CarController.instance.askDriver(methodCall.parameters[0], methodCall.parameters[1], methodCall.parameters[2]);
            }
            else
            {
                methodCall.printError();
            }
        }
        else if (methodCall.name == "isotherroadexist")
        {
            if (methodCall.parameters.Length != 0)
                methodCall.printError();
            else
            {
                bool ret = CarController.instance.isOtherRoadExist();
                methodCall.returnValue = (ret) ? 1 : 0;
            }
        }
        else if (methodCall.name == "stepgas")
        {
            if (methodCall.parameters.Length != 1)
                methodCall.printError();
            else
            {
                CarController.instance.stepGas(methodCall.parameters[0]);
            }
        }
        else if (methodCall.name == "stepclutch")
        {
            if (methodCall.parameters.Length != 1)
                methodCall.printError();
            else
            {
                CarController.instance.stepClutch(methodCall.parameters[0]);
            }
        }
        else if (methodCall.name == "changegear")
        {
            if (methodCall.parameters.Length != 1)
                methodCall.printError();
            else
            {
                CarController.instance.changeGear(methodCall.parameters[0]);
            }
        }
        else if(methodCall.name == "wait")
        {
            if (methodCall.parameters.Length != 1)
                methodCall.printError();
            else
            {
                CarController.instance.Wait(methodCall.parameters[0]);
            }
        }
        else if (methodCall.name == "getgear")
        {
            if (methodCall.parameters.Length != 0)
                methodCall.printError();
            else
            {
                int x = CarController.instance.getCurrentGear();
                methodCall.returnValue = x;
            }
        }
        else if (methodCall.name == "isrightroadavailable")
        {
            if (methodCall.parameters.Length != 0)
                methodCall.printError();
            else
            {
                int x = CarController.instance.isRightRoadAvailable();
                methodCall.returnValue = x;
            }
        }
        else if (methodCall.name == "isleftroadavailable")
        {
            if (methodCall.parameters.Length != 0)
                methodCall.printError();
            else
            {
                int x = CarController.instance.isLeftRoadAvailable();
                methodCall.returnValue = x;
            }
        }
        else if (methodCall.name == "isforwardroadavailable")
        {
            if (methodCall.parameters.Length != 0)
                methodCall.printError();
            else
            {
                int x = CarController.instance.isForwardRoadAvailable();
                methodCall.returnValue = x;
            }
        }
        else
        {
            printError("Method not found");
        }
    }

    public static int Add(int a, int b)
    {
        int sum = a + b;
        return sum;
    }
    public static void printInt(int num)
    {
        Debug.Log("Integer: " + num);
        DebugPanelController.instance.PrintMessage(num + "");
    }
    public static void printError(string error)
    {
        Debug.Log(error);
    }
}
