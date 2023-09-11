using System;
using UnityEngine;



namespace _2D_Simple_Mobile_Starter_pack.Scripts.GameObjectUtilities
{
    ///
    /// Это вам не монубехейвор
    /// Этот скрипт надо добавлять внутрь других скриптов
    /// Он создан, чтобы сводить две переменные в одну
    /// Ошибка - разница между двумя переменными
    /// То что возвращает этот скрипт - это то, что надо прибавить к переменной, чтобы она стала равна другой переменной
    /// Самое главное правильно настроить коэффициенты
    /// Погуглите пид регулятор в конце концов
    /// 
    [Serializable]
    public class PidRegulation 
    {
        public float kp, ki, kd;
        private float _integral = 0;
        private float _dV = 0;
        private float _lastError = 0;
    
        public PidRegulation(float kp, float ki, float kd)
        {
            this.kp = kp;
            this.ki = ki;
            this.kd = kd;
        }
    
        public float GetPid(float error, float deltaTime)
        {
            _integral += error * deltaTime;
            _dV = (error - _lastError) / deltaTime;
            var pid = error * kp + _integral * ki + _dV * kd;
            _lastError = error;
            return pid;
        }
        
        public float GetPid(float error)
        {
            return GetPid(error, Time.deltaTime);
        }
    }
}
