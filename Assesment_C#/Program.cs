using System;

// ===================== DELEGATES =====================

// Delegate for billing strategy
public delegate double BillingDelegate(double amount);

// Delegate for notification event
public delegate void NotificationDelegate(string message);

// ===================== PATIENT HIERARCHY =====================

// Base class (Inheritance + Polymorphism)
public abstract class Patient
{
    public int PatientId { get; set; }
    public string Name { get; set; }
    public double BillAmount { get; protected set; }

    protected Patient(int id, string name)
    {
        PatientId = id;
        Name = name;
    }

    // Polymorphic method
    public abstract void CalculateBill();
}

// InPatient
public class InPatient : Patient
{
    public int DaysAdmitted { get; set; }

    public InPatient(int id, string name, int days)
        : base(id, name)
    {
        DaysAdmitted = days;
    }

    public override void CalculateBill()
    {
        BillAmount = DaysAdmitted * 2000;
    }
}

// OutPatient
public class OutPatient : Patient
{
    public OutPatient(int id, string name)
        : base(id, name) { }

    public override void CalculateBill()
    {
        BillAmount = 500;
    }
}

// EmergencyPatient
public class EmergencyPatient : Patient
{
    public EmergencyPatient(int id, string name)
        : base(id, name) { }

    public override void CalculateBill()
    {
        BillAmount = 5000;
    }
}

// ===================== BILLING SERVICE =====================

public class BillingService
{
    public double ApplyBilling(double amount, BillingDelegate billingStrategy)
    {
        return billingStrategy(amount);
    }
}

// Billing strategies (method references)
public static class BillingStrategies
{
    public static double Regular(double amount)
    {
        return amount;
    }

    public static double Insurance(double amount)
    {
        return amount * 0.7;
    }

    public static double Emergency(double amount)
    {
        return amount * 1.2;
    }
}

// ===================== NOTIFICATION SERVICE =====================

public class HospitalNotificationService
{
    public event NotificationDelegate PatientAdmitted;

    public void Notify(string message)
    {
        if (PatientAdmitted != null)
            PatientAdmitted(message);
    }
}

// ===================== HOSPITAL SYSTEM =====================

public class HospitalSystem
{
    private BillingService billingService = new BillingService();
    private HospitalNotificationService notificationService =
        new HospitalNotificationService();

    public HospitalSystem()
    {
        // Subscribe to event
        notificationService.PatientAdmitted += DisplayNotification;
    }

    private void DisplayNotification(string message)
    {
        Console.WriteLine("[NOTIFICATION] " + message);
    }

    public void AdmitPatient(Patient patient, BillingDelegate billingStrategy)
    {
        // Step 3: Calculate treatment bill
        patient.CalculateBill();

        // Step 4: Apply billing strategy (delegate)
        double finalBill =
            billingService.ApplyBilling(patient.BillAmount, billingStrategy);

        // Step 5: Generate bill
        Console.WriteLine("\n--- BILL DETAILS ---");
        Console.WriteLine("Patient Name : " + patient.Name);
        Console.WriteLine("Final Bill   : " + finalBill);

        // Step 6: Trigger event
        notificationService.Notify(
            "Patient " + patient.Name + " admitted successfully.");
    }
}

// ===================== PROGRAM ENTRY =====================

class Program
{
    static void Main()
    {
        HospitalSystem hospital = new HospitalSystem();

        Console.WriteLine("=== Patient Management System ===");
        Console.WriteLine("1. InPatient");
        Console.WriteLine("2. OutPatient");
        Console.WriteLine("3. Emergency Patient");
        Console.Write("Select Patient Type: ");

        int choice = int.Parse(Console.ReadLine());
        Patient patient = null;

        // Step 2: Select patient type
        switch (choice)
        {
            case 1:
                patient = new InPatient(1, "John", 3);
                hospital.AdmitPatient(patient, BillingStrategies.Insurance);
                break;

            case 2:
                patient = new OutPatient(2, "Alice");
                hospital.AdmitPatient(patient, BillingStrategies.Regular);
                break;

            case 3:
                patient = new EmergencyPatient(3, "Mark");
                hospital.AdmitPatient(patient, BillingStrategies.Emergency);
                break;

            default:
                Console.WriteLine("Invalid choice");
                break;
        }

        Console.ReadLine();
    }
}
